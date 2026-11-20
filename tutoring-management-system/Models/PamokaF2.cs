namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.SutartisF2;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// 'Sutartis' in list form.
/// </summary>
public class PamokaL
{
	[Display(Name = "Id")]
	public int Id { get; set; }

	[Display(Name = "Dalykas")]
	public string Dalykas { get; set; }

	[Display(Name = "Trukmė (min)")]
	public int? Trukme { get; set; }

	
	[Display(Name = "Korepetitorius")]
	public string Korepetitorius { get; set; }

	[Display(Name = "Mokinys")]
	public string Mokinys { get; set; }
	[Display(Name = "Tvarkaraštis")]
	public string Tvarkarastis { get; set; }

	[Display(Name = "Mokėjimas")]
	public string Mokejimas { get; set; }
	[Display(Name = "Medžiagų kiekis")]
	public int MedziaguKiekis { get; set; }
}


/// <summary>
/// 'Sutartis' in create and edit forms.
/// </summary>
public class PamokaCE
{
	/// <summary>
	/// Entity data.
	/// </summary>
	public class PamokaM
	{
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Display(Name = "Dalykas")]
		[Required]
		[MaxLength(30)]
		public string Dalykas { get; set; }

		[Display(Name = "Įvertinimas")]
		public int? Ivertinimas { get; set; }

		
		[Display(Name = "Trukmė (min)")]
		[Required(ErrorMessage = "Laukas 'Trukmė' yra privalomas.")]
		[Range(1, 1000, ErrorMessage = "Trukmė turi būti teigiamas skaičius (nuo 1 iki 1000).")]
		[RegularExpression(@"^\d+$", ErrorMessage = "Prašome įvesti tik skaičius (be raidžių ir simbolių).")]
		public int? Trukme { get; set; }

		

		[Display(Name = "Įkainis")]
		[Required(ErrorMessage = "Laukas 'Įkainis' yra privalomas.")]
		[Range(0, 10000, ErrorMessage = "Įkainis negali būti neigiamas.")]
		[RegularExpression(@"^\d+$", ErrorMessage = "Prašome įvesti tik skaičius.")]
		public int? Ikainis { get; set; }
		[Display(Name = "Pradžios laikas")]
		public int? Pradzios_laikas { get; set; }

		[Display(Name = "Nuoroda")]
		[MaxLength(255)]
		public string Nuoroda { get; set; }

		
		[Display(Name = "Mokinys")]
		[Required]
		public int FkMokinys { get; set; }

		[Display(Name = "Mokėjimas")]
		[Required]
		public int FkMokejimas { get; set; }

		[Display(Name = "Korepetitorius")]
		[Required]
		public int FkKorepetitorius { get; set; }

		[Display(Name = "Tvarkaraštis")]
		[Required]
		public int FkTvarkarastis { get; set; }
		
	}

	/// <summary>
	/// Representation of 'UzsakytaPaslauga' entity in 'Sutartis' edit form.
	/// </summary>
	public class PamokosMedziagaM
	{
		/// <summary>
		/// ID of the record in the form. Is used when adding and removing records.
		/// </summary>
		public int InListId { get; set; }

		[Display(Name = "Paskirtis")]
		[MaxLength(255)]
		[Required]
		public string Paskirtis { get; set; }

		
		[Display(Name = "Naudojimosi trukmė")]
		[Range(1, 1000, ErrorMessage = "Trukmė turi būti teigiama.")]
		[RegularExpression(@"^\d+$", ErrorMessage = "Tik skaičiai.")]
		public int? NaudojimosiTrukme { get; set; }

		[Display(Name = "Pastabos")]
		[MaxLength(255)]
		public string Pastabos { get; set; }

		
		[Display(Name = "Mokomoji medžiaga")]
		[Required]
		public int FkMokomojiMedziaga { get; set; }
	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Mokiniai { get; set; }
		public IList<SelectListItem> Mokejimai { get; set; }
		public IList<SelectListItem> Korepetitoriai { get; set; }
		public IList<SelectListItem> Tvarkarastis { get; set; }

		public IList<SelectListItem> MokomosiosMedziagos { get; set; }
	}


	/// <summary>
	/// Sutartis.
	/// </summary>
	public PamokaM Pamoka { get; set; } = new PamokaM();

	/// <summary>
	/// Related 'UzsakytaPaslauga' records.
	/// </summary>
	public IList<PamokosMedziagaM> PamokosMedziagos { get; set;  } = new List<PamokosMedziagaM>();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}

/*public class SutartisL
{
	[Display(Name="Nr.")]
	public int Nr { get; set; }

	[Display(Name="Sudarymo data")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime Data { get; set; }

	[Display(Name="Darbuotojas")]
	public string Darbuotojas { get; set; }

	[Display(Name="Nuomininkas")]
	public string Nuomininkas { get; set; }

	[Display(Name="Būsena")]
	public string Busena { get; set; }
}


/// <summary>
/// 'Sutartis' in create and edit forms.
/// </summary>
public class SutartisCE
{
	/// <summary>
	/// Entity data.
	/// </summary>
	public class SutartisM
	{
		[Display(Name="Nr")]
		public int Nr { get; set; }

		[Display(Name="Data")]
		[DataType(DataType.Date)]
		[Required]
		public DateTime SutartiesData { get; set; }

		[Display(Name="Nuomos data ir laikas")]
		[DataType(DataType.DateTime)]
		[Required]
		public DateTime NuomosDataLaikas { get; set; }

		[Display(Name="Planuojama grąžinti")]
		[DataType(DataType.DateTime)]
		[Required]
		public DateTime PlanuojamaGrDataLaikas { get; set; }

		[Display(Name="Grąžinta")]
		[DataType(DataType.DateTime)]
		public DateTime? FaktineGrDataLaikas { get; set; }

		[Display(Name="Rida paimant")]
		[Required]
		public int PradineRida { get; set; }

		[Display(Name="Rida grąžinus")]
		public int? GalineRida { get; set; }

		[Display(Name="Nuomos kaina")]
		[Required]
		public decimal Kaina { get; set; }

		[Display(Name="Degalų kiekis paimant")]
		[Required]
		public int DegaluKiekisPaimant { get; set; }

		[Display(Name="Degalų kiekis gražinus")]
		public int? DegaluKiekisGrazinant { get; set; }

		[Display(Name="Būsena")]
		[Required]
		public int FkBusena { get; set; }

		[Display(Name="Klientas")]
		[Required]
		public string FkKlientas { get; set; }

		[Display(Name="Darbuotojas")]
		[Required]
		public string FkDarbuotojas { get; set; }

		[Display(Name="Automobilis")]
		[Required]
		public int FkAutomobilis { get; set; }

		[Display(Name="Gražinimo vieta")]
		[Required]
		public int FkGrazinimoVieta { get; set; }

		[Display(Name="Paėmimo vieta")]
		[Required]
		public int FkPaemimoVieta { get; set; }
	}

	/// <summary>
	/// Representation of 'UzsakytaPaslauga' entity in 'Sutartis' edit form.
	/// </summary>
	public class UzsakytaPaslaugaM
	{
		/// <summary>
		/// ID of the record in the form. Is used when adding and removing records.
		/// </summary>
		public int InListId { get; set; }

		[Display(Name="Paslauga")]
		[Required]
		public string Paslauga { get; set; }

		[Display(Name="Kiekis")]
		[Required]
		[Range(1, int.MaxValue)]
		public int Kiekis { get; set; }

		[Display(Name="Kaina")]
		[Required]
		public decimal Kaina { get; set; }
	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Busenos { get; set; }
		public IList<SelectListItem> Klientai { get; set; }
		public IList<SelectListItem> Darbuotojai { get; set; }
		public IList<SelectListItem> Automobiliai { get; set; }
		public IList<SelectListItem> Vietos { get; set; }
		public IList<SelectListItem> Paslaugos {get;set;}
	}


	/// <summary>
	/// Sutartis.
	/// </summary>
	public SutartisM Sutartis { get; set; } = new SutartisM();

	/// <summary>
	/// Related 'UzsakytaPaslauga' records.
	/// </summary>
	public IList<UzsakytaPaslaugaM> UzsakytosPaslaugos { get; set;  } = new List<UzsakytaPaslaugaM>();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}


/// <summary>
/// 'SutartiesBusena' enumerator in lists.
/// /// </summary>
public class SutartiesBusena
{
	public int Id { get; set; }

	public string Name { get; set; }
}*/