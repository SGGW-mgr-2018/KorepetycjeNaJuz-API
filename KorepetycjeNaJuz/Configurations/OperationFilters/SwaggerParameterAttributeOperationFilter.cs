using KorepetycjeNaJuz.Core.Attributes.SwaggerAttributes;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace KorepetycjeNaJuz.Configurations.OperationFilters
{
    public class SwaggerParameterAttributeParameterFilter : IParameterFilter
    {
        public void Apply(IParameter parameter, ParameterFilterContext context)
        {

            SwaggerParameterAttribute attribute = context.PropertyInfo?.GetCustomAttributes(true)
            .OfType<SwaggerParameterAttribute>()
            .FirstOrDefault();
            if(attribute != null)
            {
                if(string.IsNullOrWhiteSpace(attribute.Name) == false)
                    parameter.Name = attribute.Name;
                if(string.IsNullOrWhiteSpace(attribute.Description) == false)
                    parameter.Description = attribute.Description;
            }
            //parameter.In = attribute.ParameterType;
        }
    }
}
