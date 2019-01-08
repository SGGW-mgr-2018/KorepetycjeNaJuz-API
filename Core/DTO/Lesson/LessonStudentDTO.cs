using System;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class LessonStudentDTO
    {
        /// <summary>
        /// Id lekcji
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id statusu lekcji
        /// </summary>
        public int LessonStatusId { get; set; }

        /// <summary>
        /// Nazwa statusu lekcji
        /// </summary>
        public string LessonStatusName { get; set; }

        /// <summary>
        /// Uczeń
        /// </summary>
        public UserDTO Student { get; set; }
    }
}
