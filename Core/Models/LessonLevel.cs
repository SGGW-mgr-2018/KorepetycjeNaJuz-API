using KorepetycjeNaJuz.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Models
{
    public class LessonLevel : IEntityWithTypedId<int>
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string LevelName { get; set; }

        public virtual ICollection<CoachLessonLevel> CoachLessons { get; set; }
    }
}
