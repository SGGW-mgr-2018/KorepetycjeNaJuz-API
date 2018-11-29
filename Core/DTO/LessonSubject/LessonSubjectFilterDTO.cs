using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.DTO
{
	public class LessonSubjectFilterDTO
	{
		/// <summary>
		/// Filtr na nazwę przedmiotu.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Konwertuje do JSON.
		/// </summary>
		/// <returns>JSON.</returns>
		public override string ToString()
		{
			return $"{{ Name: \"{Name}\" }}";
		}
	}
}
