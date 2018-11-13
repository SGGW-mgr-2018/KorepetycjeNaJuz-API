using KorepetycjeNaJuz.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Models
{
    public class Address : IEntityWithTypedId<int>
    {
        [Key]
        public int Id { get; set; }

        public int CoachId { get; set; }
        public virtual User Coach { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(250)]
        public string Street { get; set; }
    }
}
