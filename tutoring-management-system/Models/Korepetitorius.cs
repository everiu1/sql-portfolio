namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Model of 'Klientas' entity.
/// </summary>
public class Korepetitorius
{
	[Display(Name = "ID")]
	public int Id { get; set; }

	[Display(Name="Vardas")]
	[Required]
	[MaxLength(30)]
	public string Vardas { get; set; }
	
	[Display(Name="Pavardė")]
	[Required]
	[MaxLength(30)]
	public string Pavarde { get; set; }

	[Display(Name="El. paštas")]
	[Required]
	[MaxLength(50)]
	public string El_pastas { get; set; }

	

	[Display(Name="Tel. numeris")]
	[Required]
	[MaxLength(13)]
	public string Tel_numeris { get; set; }

	[Display(Name = "Valandos per savaitę")]
	public int? Valandos_per_savaite { get; set; }
	[Display(Name = "Aprašymas")]
	[MaxLength(255)]
	public string Aprasymas { get; set; }
	[Display(Name = "Mokymo patirtis")]
	public int? Mokymo_patirtis { get; set; }




}
