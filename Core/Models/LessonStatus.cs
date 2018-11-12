using KorepetycjeNaJuz.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Models
{
    public class LessonStatus : IEntityWithTypedId<int>
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}
