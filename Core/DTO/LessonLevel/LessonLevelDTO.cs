using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.DTO
{
	public class LessonLevelDTO
	{
		/// <summary>
		/// Identyfikator poziomu lekcji.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Nazwa poziomu lekcji.
		/// </summary>
		[Required(ErrorMessage = "Nazwa jest wymagana.")]
		[MinLength(1, ErrorMessage = "Nazwa musi zawierać conajmniej 1 znak.")]
		[MaxLength(255, ErrorMessage = "Nazwa może zawierać maksymalnie 255 znaków.")]
		public string LevelName { get; set; }

		/// <summary>
		/// Konwertuje do JSON.
		/// </summary>
		/// <returns>JSON.</returns>
		public override string ToString()
		{
			return $"{{ Id: {Id}, Name: \"{LevelName}\" }}";
		}
	}
}
