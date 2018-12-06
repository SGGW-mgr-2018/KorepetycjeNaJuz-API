using Microsoft.AspNetCore.Builder;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Collections.Generic;
using KorepetycjeNaJuz.Configurations.OperationFilters;

namespace KorepetycjeNaJuz.Configurations
{
    public class SwaggerConfiguration
    {
        private const string SwaggerUiEndpoint = "/swagger/v1/swagger.json";
        private const string ApiVersion = "v1";
        private const string XmlExtension = ".xml";
        private const string AuthorizationType = "Bearer";
        private const string ApiKeySchemeName = "Authorization";
        private const string ApiKeySchemeIn = "header";
        private const string ApiKeySchemeType = "apiKey";


        public static void RegisterService(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersion, new Info { Title = "Korepetycje na już API", Version = ApiVersion });
                c.AddSecurityDefinition(AuthorizationType, new ApiKeyScheme
                {
                    Description = "Autoryzacja za pomocą Json Web Token. Przykład użycia: \"Bearer {token}\"",
                    Name = ApiKeySchemeName,
                    In = ApiKeySchemeIn,
                    Type = ApiKeySchemeType
                });                
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
                
                c.DescribeAllEnumsAsStrings();
                c.IgnoreObsoleteProperties();
                c.IncludeXmlComments(CreateXmlCommentsPath());

                // Operation Filters
                c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();
                c.ParameterFilter<SwaggerParameterAttributeParameterFilter>();
            });
        }

        public static void RegisterUi(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerUiEndpoint, "");
            });
        }

        private static string CreateXmlCommentsPath()
        {
            string applicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string xmlCommentsFileName = applicationName + XmlExtension;

            string basePath = ApplicationEnvironment.ApplicationBasePath;
            string xmlPath = Path.Combine(basePath, xmlCommentsFileName);

            return xmlPath;
        }

    }
}
