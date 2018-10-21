using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Data.Models
{
    public class CoachAddresses
    {
        [Key]
        public int Id { get; set; }

        public int CoachId { get; set; }
        public Users Coach { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [MaxLength(1000)]
        public string Address { get; set; }
    }
}
