using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Repository;
using GameManager.WebApp.BS.Service;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Authorization.Interfaces;
using GameManager.WebApp.BS.Authorization.Implementations;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;

namespace GameManager.WebApp.BS.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services, string allowedOrigins) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins(allowedOrigins.Split(','))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) => 
            services.Configure<IISOptions>(options =>
            {

            });

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServices(this IServiceCollection services) {
            services.AddScoped<IAuth0Service, Auth0Service>();
            services.AddScoped<IBillinAddressService, BillingAddressService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ILocaleService, LocaleService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IResourceGroupService, ResourceGroupService>();
            services.AddScoped<IProductSubscriptionAffService, ProductSubscriptionAffService>();
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<ICertificateService, CertificateService>();
            services.AddScoped<ICertificateTypeService, CertificateTypeService>();
            services.AddScoped<IInvoiceTaxRateService, InvoiceTaxRateService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IApiAccessTokenService, ApiAccessTokenService>();
            services.AddScoped<IFaqCategoryService, FaqService>();
            services.AddScoped<IUserDocumentService, UserDocumentService>();
            services.AddScoped<IMonitoringService, MonitoringService>();
            services.AddScoped<IDemoService, DemoService>();
            services.AddScoped<IScriptsService, ScriptsService>();
            services.AddScoped<IProductConfigurationService, ProductConfigurationService>();
            services.AddScoped<IBlobStorageBrowserService, BlobStorageBrowserService>();
            services.AddScoped<IBlobDownloadService, BlobStorageDownloadService>();
            services.AddScoped<INotificationService, NotificationService>();
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddScoped<IAccessRightsResolver, AccessRightsResolver>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")),ServiceLifetime.Transient);

        public static void ConfigureSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(s =>
            {
                s.MapType<DateOnly>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "date",
                    Example = new OpenApiString("2022-01-01")
                });
                s.SchemaFilter<EnumSchemaFilter>();
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Fiscal Cloud API",
                    Version = "v1",
                });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
    }
}
