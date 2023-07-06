using GameManager.WebApp.BS.Shared.ConfigurationOptions;

namespace GameManager.WebApp.BS.API.Extensions
{
    public static class OptionExtensions
    {
        public static IServiceCollection ConfigureAppOptions(this IServiceCollection service, IConfiguration configuration)
        {
            return service.Configure<DemoMasterDataOptions>(configuration.GetSection(DemoMasterDataOptions.DemoMasterData))
                          .Configure<UserManagedIdentityOptions>(configuration.GetSection(UserManagedIdentityOptions.UserManagedIdentity));
        }
    }
}
