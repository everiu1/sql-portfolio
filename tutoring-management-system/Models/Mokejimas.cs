namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Model for 'Marke mokejimas ' entity.
/// </summary>
public class Mokejimas
{
	[Display(Name = "ID")]
	public int Id { get; set; }

	[Display(Name = "Suma")]
	public int? Suma { get; set; } 

	[Display(Name = "Paskirtis")]
	[MaxLength(50)] 
	public string Paskirtis { get; set; }

	[Display(Name = "Data")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	public DateTime? Data { get; set; }

	[Display(Name = "Sąskaitos numeris")]
	[Required] 
	[MaxLength(200)]
	public string Saskaitos_numeris { get; set; }

	[Display(Name = "Mokinys")]
	[Required] 
	public int Fk_Mokinys { get; set; }
}
