using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.DTO
{
	public class LessonLevelCreateDTO
	{
		/// <summary>
		/// Nazwa poziomu lekcji.
		/// </summary>
		[Required(ErrorMessage = "Nazwa jest wymagana.")]
		[MinLength(2, ErrorMessage = "Nazwa musi zawierać conajmniej 2 znaki.")]
		[MaxLength(50, ErrorMessage = "Nazwa może zawierać maksymalnie 50 znaków.")]
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
