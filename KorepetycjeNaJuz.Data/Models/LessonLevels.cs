using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Data.Models
{
    public class LessonLevels
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string LevelName { get; set; }
    }
}
