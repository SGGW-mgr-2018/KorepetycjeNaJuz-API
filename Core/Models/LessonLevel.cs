using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Models
{
    public class LessonLevel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string LevelName { get; set; }
    }
}
