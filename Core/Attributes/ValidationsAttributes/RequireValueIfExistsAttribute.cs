using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Attributes.ValidationsAttributes
{
    public class RequireValueIfExistsAttribute : ValidationAttribute
    {
        private readonly string _requiredValue;

        public RequireValueIfExistsAttribute(string value)
        {
            _requiredValue = value;
        }

        public override bool IsValid(object value)
        {
            if (value != null && _requiredValue == value.ToString())
            {
                return true;
            }
            else if (value == null)
            {
                return true;
            }

            return false;
        }
    }
}
