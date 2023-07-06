using AutoMapper;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using GameManager.WebApp.BS.Authorization.IdentityTools;
using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Shared.ConfigurationOptions;
using GameManager.WebApp.BS.Shared.Constants;
using GameManager.WebApp.BS.Shared.DataTransferObjects.ApiAccessToken;
using GameManager.WebApp.BS.Shared.Exceptions.ApiAccessToken;
using GameManager.WebApp.BS.Shared.Exceptions.Auth0;
using GameManager.WebApp.BS.Shared.Exceptions.User;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GameManager.WebApp.BS.Service
{
    public class ApiAccessTokenService :IApiAccessTokenService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly KeyVaultOptions _keyVaultOptions;
        private readonly SecretClient _secretClient;
        private readonly ILogger<ApiAccessTokenService> _logger;

        private const int Keysize = 128;
        private const int DerivationIterations = 1000;

        public ApiAccessTokenService(IRepositoryManager repository,IMapper mapper, ILogger<ApiAccessTokenService> logger,
            IOptions<KeyVaultOptions> kvOptions)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _keyVaultOptions = kvOptions.Value ?? throw new ArgumentNullException(nameof(kvOptions));
            _secretClient = new SecretClient(vaultUri: new Uri(_keyVaultOptions.KeyVaultURI), credential: new DefaultAzureCredential());
        }    

        public async Task<(IEnumerable<ApiAccessTokenDto> apiAccessTokenDto, MetaData metaData)> GetApiAccessTokensAsync(ApiAccessTokenParameters apiAccessTokenParam, bool trackChanges, ClaimsPrincipal user)
        {
            int categoryIDNumber = GetCategoryId(user);

            string subscriptionID = ClaimsParser.ParseClaim(user, TokenClaims.SubscriptionId);

            if (categoryIDNumber == Shared.Constants.UserCategory.Internal)
            {
                var apiAccessTokenWithMetaData = await _repository.ApiAccessTokenRepository.GetAllApiAccessTokenAsync(apiAccessTokenParam, trackChanges);
                return MapApiAccessToken(apiAccessTokenWithMetaData);
            }

            if (categoryIDNumber == Shared.Constants.UserCategory.Partner || categoryIDNumber == Shared.Constants.UserCategory.EndUser)
            {
                var apiAccessTokenWithMetaData = await _repository.ApiAccessTokenRepository.GetApiAccessTokenForExternalUserAsync(apiAccessTokenParam, trackChanges, subscriptionID);
                return MapApiAccessToken(apiAccessTokenWithMetaData);
            }

            throw new UnknownUserCategoryException($"Unknown categoryID: {categoryIDNumber}");

        }

        private static int GetCategoryId(ClaimsPrincipal user)
        {
            string categoryID = ClaimsParser.ParseClaim(user, TokenClaims.UserCategory);

            int categoryIDNumber = 0;
            if (!int.TryParse(categoryID, out categoryIDNumber))
            {
                throw new TokenInvalidException($"Claim categoryID is not a valid integer.");
            }

            return categoryIDNumber;
        }

        private (IEnumerable<ApiAccessTokenDto> apiAccessTokenDto, MetaData metaData) MapApiAccessToken(PagedList<ApiAccessToken> apiAccessTokenWithMetaData)
        {
            var apiAccessTokenDTO = _mapper.Map<IEnumerable<ApiAccessTokenDto>>(apiAccessTokenWithMetaData);
            return (apiAccessTokenDto: apiAccessTokenDTO, metaData: apiAccessTokenWithMetaData.MetaData);
        }
    

        public async Task<ApiAccessTokenDto> CreateApiAccessTokenAsync(ApiAccessTokenForCreationDto apiAccessToken)
        {
            var encriptedData = PrepeareDataAndSaveToKeyVault(apiAccessToken);

            var apiAccessTokenEntity = _mapper.Map<ApiAccessToken>(apiAccessToken);
            var secretName = apiAccessToken.LoginId;

            apiAccessTokenEntity.SKeyVaultSecretId = secretName;
            _repository.ApiAccessTokenRepository.CreateApiAccessToken(apiAccessTokenEntity);
            await _repository.SaveAsync();       

            try
            {
                await _secretClient.SetSecretAsync(secretName, encriptedData);
            }
            catch
            {
                _repository.ApiAccessTokenRepository.DeleteApiAccessToken(apiAccessTokenEntity);
                await _repository.SaveAsync();
                throw;
            }

            var apiAccessTokenToReturn = _mapper.Map<ApiAccessTokenDto>(apiAccessTokenEntity);

            return apiAccessTokenToReturn;
        }

        public async Task<ApiAccessTokenDto> GetApiAccessTokenByIdAsync(int id, bool trackChanges)
        {
            var apiAccessToken = await _repository.ApiAccessTokenRepository.GetApiAccessTokenByIdAsync(id, trackChanges);

            if (apiAccessToken is null)
            {
                throw new ApiAccessTokenNotFoundException(id.ToString());
            }

            var apiAccessTokenDto = _mapper.Map<ApiAccessTokenDto>(apiAccessToken);
            return apiAccessTokenDto;
        }

        private string PrepeareDataAndSaveToKeyVault(ApiAccessTokenForCreationDto apiAccessToken)
        {
           string dataToEncript = "{\"subID\": \"" + apiAccessToken.SubscriptionId + "\",\"clientId\": \""
                                    + apiAccessToken.LoginId + "\",\"created\": \"" 
                                    + DateTime.Now.ToString("yyyy-MM-dd") + " \",\"expire\" : \"" 
                                    + apiAccessToken.ExpireDate.Value.ToString("yyyy-MM-dd HH:MM") + "\"}";

           return Encrypt(dataToEncript,EncryptRfc2898.PassPhrase);

        }

        public async Task DeleteApiAccessTokenAsync(int id, bool trackChanges)
        {
            var apiAccessToken = await _repository.ApiAccessTokenRepository.GetApiAccessTokenByIdAsync(id, trackChanges);
            if (apiAccessToken is null)
            {
                throw new ApiAccessTokenNotFoundException(id.ToString());
            }

            try
            {   
                var operation = await _secretClient.StartDeleteSecretAsync(apiAccessToken.SKeyVaultSecretId);
                await operation.WaitForCompletionAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to delete api access token with name {apiAccessToken.SKeyVaultSecretId} from key vault. Error: ${e.Message}");
            }

            try
            {
                _repository.ApiAccessTokenRepository.DeleteApiAccessToken(apiAccessToken);
                await _repository.SaveAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to delete api access token with name {apiAccessToken.SKeyVaultSecretId} from key vault. Error: ${e.Message}");
                throw new Exception("Unable to delete access token from database.");
            }
        }



        private static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate128BitsOfRandomEntropy();
            var ivStringBytes = Generate128BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = Aes.Create())
                {
                    symmetricKey.BlockSize = 128;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;

                    using var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes);
                    using var memoryStream = new MemoryStream();

                    using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                    var cipherTextBytes = saltStringBytes;
                    cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                    cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                    memoryStream.Close();
                    cryptoStream.Close();

                    return Convert.ToBase64String(cipherTextBytes);
                }
            }
        }

        private static byte[] Generate128BitsOfRandomEntropy()
        {
            var randomBytes = new byte[16]; // 16 Bytes will give us 128 bits.
            using (var rngCsp = RandomNumberGenerator.Create())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        public async Task<string> GetTokenFromKeyVault(int id, ClaimsPrincipal user)
        {
            string subscriptionID = ClaimsParser.ParseClaim(user, TokenClaims.SubscriptionId);

            var apiAccessToken = await _repository.ApiAccessTokenRepository.GetApiAccessTokenByIdAsync(id, false);
            if (apiAccessToken is null)
            {
                throw new ApiAccessTokenNotFoundException(id.ToString());
            }

            int categoryIDNumber = GetCategoryId(user);

            if (categoryIDNumber != Shared.Constants.UserCategory.Internal && !apiAccessToken.SSubscriptionId.Equals(subscriptionID))
            {
                string userName = ClaimsParser.ParseClaim(user, TokenClaims.Username);
                _logger.LogError($"User {userName} with subscriptionId {subscriptionID} try to retrieve token with {apiAccessToken.SSubscriptionId} subscriptionId");
                throw new UserAndTokenSubscriptionIdAreNotEqual(userName, subscriptionID, apiAccessToken.IApiAccessTokenId);
            }

            try
            {
                var operation = await _secretClient.GetSecretAsync(apiAccessToken.SKeyVaultSecretId);
                string token = operation.Value.Value.ToString();
                return token;
         
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to retrieve api access token with name {apiAccessToken.SKeyVaultSecretId}. Error: ${e.Message}");
                return string.Empty;
            }

        }
      
    }
}
