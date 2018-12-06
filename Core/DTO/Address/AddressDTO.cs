using System;
using System.Collections.Generic;
using System.Text;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class AddressDTO
    {
        /// <summary>
        /// Szerokość geograficzna
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Długość geograficzna
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Miasto
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Ulica
        /// </summary>
        public string Street { get; set; }
    }
}
