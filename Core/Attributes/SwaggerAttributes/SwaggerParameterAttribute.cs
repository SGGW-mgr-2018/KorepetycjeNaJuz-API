using System;

namespace KorepetycjeNaJuz.Core.Attributes.SwaggerAttributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class SwaggerParameterAttribute : Attribute
    {
        public SwaggerParameterAttribute(string name = "", string description="")
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
