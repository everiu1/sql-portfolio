namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Model of 'Aikstele i tvarkarastis' entity.
/// </summary>
public class Tvarkarastis
{
	[Display(Name = "ID")]
	public int Id { get; set; }

	[Display(Name = "Data")]
	[Required] 
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	public DateTime Data { get; set; }

	[Display(Name = "Mokinys")]
	[Required] 
	public int Fk_Mokinys { get; set; }
}