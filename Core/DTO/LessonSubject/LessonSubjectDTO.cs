using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.DTO
{
	public class LessonSubjectDTO
	{
		/// <summary>
		/// Identyfikator przedmiotu.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Nazwa przedmiotu.
		/// </summary>
		[Required(ErrorMessage = "Nazwa jest wymagana.")]
		[MinLength(2, ErrorMessage = "Nazwa musi zawierać conajmniej 2 znaki.")]
		[MaxLength(50, ErrorMessage = "Nazwa może zawierać maksymalnie 50 znaków.")]
		public string Name { get; set; }

		/// <summary>
		/// Konwertuje do JSON.
		/// </summary>
		/// <returns>JSON.</returns>
		public override string ToString()
		{
			return $"{{ Id: {Id}, Name: \"{Name}\" }}";
		}
	}
}
