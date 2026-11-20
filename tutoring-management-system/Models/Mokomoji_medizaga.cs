namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Paslauga;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Model of 'Paslauga' entity.
/// </summary>
public class Mokomoji_medizaga
{
	[Display(Name = "ID")]
	public int Id { get; set; }

	[Display(Name = "Dalykas")]
	[Required] 
	[MaxLength(255)]
	public string Dalykas { get; set; }

	[Display(Name = "Pavadinimas")]
	[Required] 
	[MaxLength(255)]
	public string Pavadinimas { get; set; }

	[Display(Name = "Klasė")]
	[MaxLength(255)] 
	public string Klase { get; set; }

	[Display(Name = "Tipas")]
	[MaxLength(255)]
	public string Tipas { get; set; }

	[Display(Name = "Dalis")]
	public int? Dalis { get; set; } 
}


/// <summary>
/// View model for editing data of 'Paslauga' entity.
/// </summary>
public class PaslaugaCE
{
	/// <summary>
	/// Entity data.
	/// </summary>
	public Mokomoji_medizaga MokomojiMedizaga { get; set; } = new Mokomoji_medizaga();
}
