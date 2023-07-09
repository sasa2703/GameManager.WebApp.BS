using GameManager.WebApp.BS.Authorization.Implementations;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Shared.Constants;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Role;
using GameManager.WebApp.BS.Shared.DataTransferObjects.User;
using GameManager.WebApp.BS.Shared.Exceptions.Auth0;
using GameManager.WebApp.BS.Shared.Exceptions.Authorization;
using GameManager.WebApp.BS.Shared.Exceptions.User;
using Moq;
using System.Security.Claims;
using Xunit;

namespace GameManager.WebApp.BS.Authorization.Tests.Implementations
{
    public class AccessRightsResolverTests
    {
        private readonly Mock<IUserService> _mockUser = new Mock<IUserService>();

        [Fact]
        public void CheckPrincipalsRightsOnSubscription__NullSubscriptionEndUser__ThrowsUnauthorized()
        {
            // Arrange
            AccessRightsResolver resolver = new AccessRightsResolver(_mockUser.Object);
            string subID = null;
            var claims = new List<Claim>()
            {
                new Claim(TokenClaims.UserCategory, UserCategory.EndUser.ToString()),
                new Claim(TokenClaims.SubscriptionId, "456"),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            //Act assert
            Assert.Throws<InsufficientSubscriptionException>(() => resolver.CheckPrincipalsRightsOnSubscription(claimsPrincipal, subID));
        }

        [Fact]
        public void CheckPrincipalsRightsOnSubscription__InvalidCategoryID__ThrowsTokenInvalidException()
        {
            // Arrange
            AccessRightsResolver resolver = new AccessRightsResolver(_mockUser.Object);
            string subID = "123";
            var claims = new List<Claim>()
            {
                new Claim(TokenClaims.UserCategory, "somestring"),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            //Act assert
            Assert.Throws<TokenInvalidException>(() => resolver.CheckPrincipalsRightsOnSubscription(claimsPrincipal, subID));
        }

        [Fact]
        public void CheckPrincipalsRightsOnSubscription__InternalUser__AssertDoesntThrow()
        {
            // Arrange
            AccessRightsResolver resolver = new AccessRightsResolver(_mockUser.Object);
            string subID = "123";
            var claims = new List<Claim>()
            {
                new Claim(TokenClaims.UserCategory, UserCategory.Internal.ToString()),
                new Claim(TokenClaims.SubscriptionId, "456"),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            //Act assert
            resolver.CheckPrincipalsRightsOnSubscription(claimsPrincipal, subID);
        }
        [Fact]
        public void CheckPrincipalsUsername__MatchingUsername__DoesntThrow()
        {
            // Arrange
            AccessRightsResolver resolver = new AccessRightsResolver(_mockUser.Object);
            string passedUsername = "Aca";
            var claims = new List<Claim>()
            {
                new Claim(TokenClaims.Username, "Aca"),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            //Act assert
             resolver.CheckPrincipalsUsername(claimsPrincipal, passedUsername);
        }

        [Fact]
        public void CheckPrincipalsUsername__NonMatchingUsername__ThrowUnauthorized()
        {
            // Arrange
            AccessRightsResolver resolver = new AccessRightsResolver(_mockUser.Object);
            string passedUsername = "Djape";
            var claims = new List<Claim>()
            {
                new Claim(TokenClaims.Username, "Aca"),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            //Act assert
            Assert.Throws<InvalidPrincipalUsernameException>(() => resolver.CheckPrincipalsUsername(claimsPrincipal, passedUsername));
        }

        [Fact]
        public async void CheckPrincipalsRightsOnRole__IncompatibleRole__ThrowsUnauthorizedException()
        {
            // Arrange
            string subID = "456";
            string username = "Aca";
            UserDto user = new UserDto 
            { Role =  new RoleDto
                {
                    RoleId = 1,
                    RoleName = "Somerole"
                }
            };

            _mockUser.Setup(x => x.GetUserByUsernameAsync(username, false)).Returns(async () => user);
            AccessRightsResolver resolver = new AccessRightsResolver(_mockUser.Object);
            var claims = new List<Claim>()
            {
                new Claim(TokenClaims.Username, username),
                new Claim(TokenClaims.UserCategory, UserCategory.Partner.ToString()),
                new Claim(TokenClaims.SubscriptionId, "456"),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            //Act assert
            await Assert.ThrowsAsync<InsufficientRoleException>(() =>  resolver.CheckPrincipalsRightsOnRole(claimsPrincipal, 2));
        }

        [Fact]
        public async void CheckPrincipalsRightsOnRole__CompatibleRole__AssertDoesntThrow()
        {
            // Arrange
            string subID = "456";
            string username = "Aca";
            UserDto user = new UserDto
            {
                Role = new RoleDto
                {
                    RoleId = 2,
                    RoleName = "Somerole"
                }
            };

            _mockUser.Setup(x => x.GetUserByUsernameAsync(username, false)).Returns(async () => user);
            AccessRightsResolver resolver = new AccessRightsResolver(_mockUser.Object);
            var claims = new List<Claim>()
            {
                new Claim(TokenClaims.Username, username),
                new Claim(TokenClaims.UserCategory, UserCategory.Partner.ToString()),
                new Claim(TokenClaims.SubscriptionId, "456"),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            //Act assert
            await resolver.CheckPrincipalsRightsOnRole(claimsPrincipal, 2);
        }

        [Fact]
        public async void CheckPrincipalsRightsOnDelete__SelfDelete__ThrowsPrincipalSelfDeleteException()
        {
            // Arrange
            string subID = "456";
            string username = "Aca";
            UserDto user = new UserDto
            {
                Username = "Aca",
                Role = new RoleDto
                {
                    RoleId = 2,
                    RoleName = "Somerole"
                },
                SubscriptionId = "456"
            };

            _mockUser.Setup(x => x.GetUserByUsernameAsync(username, false)).Returns(async () => user);
            AccessRightsResolver resolver = new AccessRightsResolver(_mockUser.Object);
            var claims = new List<Claim>()
            {
                new Claim(TokenClaims.Username, username),
                new Claim(TokenClaims.UserCategory, UserCategory.Partner.ToString()),
                new Claim(TokenClaims.SubscriptionId, "456"),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            //Act assert
            await Assert.ThrowsAsync<PrincipalSelfDeleteException>(() => resolver.CheckPrincipalsRightsOnDelete(claimsPrincipal, "Aca"));
        }

        [Fact]
        public async void CheckPrincipalsRightsOnDelete__InsufficientSubscription__ThrowsInsufficientSubscriptionException()
        {
            // Arrange
            string subID = "123";
            string username = "Aca";
            UserDto user = new UserDto
            {
                Username = "Joca",
                Role = new RoleDto
                {
                    RoleId = 2,
                    RoleName = "Somerole"
                },
                SubscriptionId = "456"
            };

            _mockUser.Setup(x => x.GetUserByUsernameAsync(user.Username, false)).Returns(async () => user);
            AccessRightsResolver resolver = new AccessRightsResolver(_mockUser.Object);
            var claims = new List<Claim>()
            {
                new Claim(TokenClaims.Username, username),
                new Claim(TokenClaims.UserCategory, UserCategory.EndUser.ToString()),
                new Claim(TokenClaims.SubscriptionId, subID),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            //Act assert
            await Assert.ThrowsAsync<InsufficientSubscriptionException>(() => resolver.CheckPrincipalsRightsOnDelete(claimsPrincipal, "Joca"));
        }
    }
}
