using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.ValidationsAttributes
{
    public class MinElementsAttribute : ValidationAttribute
    {
        private readonly int _minElements;

        public MinElementsAttribute(int minElements)
        {
            _minElements = minElements;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count >= _minElements;
            }

            return false;
        }
    }
}
