using System;
using System.Collections.Generic;
using System.Text;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class AddressDTO
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string City { get; set; }

        public string Street { get; set; }
    }
}
