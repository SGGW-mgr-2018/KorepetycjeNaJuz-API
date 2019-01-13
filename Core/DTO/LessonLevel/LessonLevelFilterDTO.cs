using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.DTO
{
	public class LessonLevelFilterDTO
	{
		/// <summary>
		/// Filtr na nazwę poziomu lekcji.
		/// </summary>
		public string LevelName { get; set; }

		/// <summary>
		/// Konwertuje do JSON.
		/// </summary>
		/// <returns>JSON.</returns>
		public override string ToString()
		{
			return $"{{ Name: \"{LevelName}\" }}";
		}
	}
}
