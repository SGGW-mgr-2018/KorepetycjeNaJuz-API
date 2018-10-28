using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Models
{
    public class CoachAddress
    {
        [Key]
        public int Id { get; set; }

        public int CoachId { get; set; }
        public User Coach { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [MaxLength(1000)]
        public string Address { get; set; }
    }
}
