using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace KorepetycjeNaJuz.Configurations.OperationFilters
{
    public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            var attribute = context.MethodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false);
            if (attribute.Any())
            {
                operation.Parameters.Add(new HeaderParameter()
                {
                    Name = "Authorization",
                    In = "header",
                    Description = "JWT Bearer Token",
                    Type = "string",
                    Required = true,
                    Default = "Bearer en..."
                });
            }
        }

        class HeaderParameter : NonBodyParameter
        {

        }
    }
}
