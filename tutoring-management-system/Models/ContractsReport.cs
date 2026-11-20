namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.ContractsReport;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// View model for single contract in a report.
/// </summary>
public class Pamoka
{
	[Display(Name = "Pamokos Nr.")]
	public int PamokosNr { get; set; }

	[Display(Name = "Dalykas")]
	public string Dalykas { get; set; }

	
	public string Korepetitorius { get; set; }

	public int PatirtisMetais { get; set; }

	[Display(Name = "Pamokos trukmė")]
	public int PamokosTrukme { get; set; }

	[Display(Name = "Įvertinimas")]
	public int Ivertinimas { get; set; }

	[Display(Name = "Medžiagų skaičius")]
	public int PanaudotuMedziaguSkaicius { get; set; }

	[Display(Name = "Medžiagų naud. trukmė")]
	public int BendraMedziaguTrukme { get; set; }
	[Display(Name = "Vid. medž. trukmė")]
	public decimal VidutineMedziagosTrukme { get; set; }

	[Display(Name = "Ilgiausias naud.")] 
	public int IlgiausiasMedziagosNaudojimas { get; set; }

	

}

/// <summary>
/// View model for whole report.
/// </summary>
/// tie uzrasai prie filtravimo
public class Report
{
	[Display(Name = "Trukmė nuo:")]
	[Range(0, int.MaxValue, ErrorMessage = "Trukme negali būti neigiama")]
	public int? TrukmeNuo { get; set; }

	[Display(Name = "Trukmė iki:")]
	[Range(0, int.MaxValue, ErrorMessage = "Trukme negali būti neigiama")]
	public int? TrukmeIki { get; set; }

	[Display(Name = "Min. įvertinimas:")]
	[Range(0, int.MaxValue, ErrorMessage = "Įvertinimas negali būti neigiamas")]
	public int? MinIvertinimas { get; set; }

	[Display(Name = "Min. patirtis (metai):")]
	[Range(0, int.MaxValue, ErrorMessage = "Patirtis negali būti neigiama")]
	public int? MinPatirtis { get; set; }

	
	public List<Pamoka> Pamokos { get; set; }

	public int VisoPanaudotaMedziagu { get; set; }

	public int BendraVisuMedziaguTrukme { get; set; }
}
/*public class Sutartis
{
	[Display(Name="Sutartis")]
	public int Nr { get; set; }

	[Display(Name="Data")]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime SutartiesData { get; set; }

	public string Vardas { get; set; }

	public string Pavarde { get; set; }

	public string AsmensKodas { get; set; }

	[Display(Name="Sudarytų sutarčių vertė")]
	public decimal Kaina { get; set; }

	[Display(Name="Užsakytų paslaugų vertė")]
	public decimal PaslauguKaina { get; set; }

	public decimal BendraSuma { get; set; }

	public decimal BendraSumaPaslaug { get; set; }
}

/// <summary>
/// View model for whole report.
/// </summary>
public class Report
{
	[DataType(DataType.DateTime)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? DateFrom { get; set; }

	[DataType(DataType.DateTime)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? DateTo { get; set; }

	public List<Sutartis> Sutartys { get; set; }

	public decimal VisoSumaSutartciu { get; set; }

	public decimal VisoSumaPaslaugu { get; set; }
}
*/