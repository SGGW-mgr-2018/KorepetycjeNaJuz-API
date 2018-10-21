using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Data.Models
{
    public class LessonStatuses
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}
