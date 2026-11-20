namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Modelis;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Model of 'Modelis' entity.
/// </summary>
public class Mokinys
{
	[Display(Name="Id")]
	public int Id { get; set; }

	[Display(Name="Vardas")]
	[Required]
	[MaxLength(30)]
	public string Vardas { get; set; }

	[Display(Name = "Pavardė")]
	[Required]
	[MaxLength(30)]
	public string Pavarde { get; set; }

	[Display(Name = "Klasė")]
	public int? Klase { get; set; } 

	[Display(Name = "El. paštas")]
	[Required]
	[MaxLength(50)]
	public string El_pastas { get; set; }

	[Display(Name = "Tel. numeris")]
	[MaxLength(13)]
	public string Tel_numeris { get; set; }

	[Display(Name = "Registracijos data")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? Registracijos_data { get; set; }


	[Display(Name = "Mokinio atstovas")]
	[Required]
	public int FkMokinioAtstovas { get; set; }
}
/*public class Modelis
{
	[Display(Name="Id")]
	public int Id { get; set; }

	[Display(Name="Pavadinimas")]
	public string Pavadinimas { get; set; }

	//Markė
	[Display(Name="Markė")]
	public int FkMarke { get; set; }
}*/


/// <summary>
/// Model of 'Modelis' entity used in lists.
/// </summary>
public class MokinysL
{
	[Display(Name = "Id")]
	public int Id { get; set; }

	[Display(Name = "Vardas")]
	public string Vardas { get; set; }

	[Display(Name = "Pavardė")]
	public string Pavarde { get; set; }

	[Display(Name = "Klasė")]
	public int? Klase { get; set; }

	[Display(Name = "Mokinio atstovas")]
	public string Atstovas { get; set; }
}

/*public class ModelisL
{
	[Display(Name="Id")]
	public int Id { get; set; }

	[Display(Name="Pavadinimas")]
	public string Pavadinimas { get; set; }		

	[Display(Name="Markė")]
	public string Marke { get; set; }
}*/
/// <summary>
/// Model of 'Modelis' entity used in creation and editing forms.
/// </summary>
public class MokinysCE
{
	/// <summary>
	/// Entity data
	/// </summary>
	public class MokinysM
	{
		[Display(Name="Id")]
		public int Id { get; set; }

		[Display(Name = "Vardas")]
		[MaxLength(30)]
		[Required]
		public string Vardas { get; set; }

		[Display(Name = "Pavardė")]
		[MaxLength(30)]
		[Required]
		public string Pavarde { get; set; }

		[Display(Name = "Klasė")]
		public int? Klase { get; set; }

		[Display(Name = "El. paštas")]
		[MaxLength(50)]
		[Required]
		public string El_pastas { get; set; }

		[Display(Name = "Tel. numeris")]
		[MaxLength(13)]
		public string Tel_numeris { get; set; }

		[Display(Name = "Registracijos data")]
		[DataType(DataType.Date)]
		public DateTime? Registracijos_data { get; set; }

		[Display(Name = "Mokinio atstovas")]
		[Required]
		public int FkMokinioAtstovas { get; set; }
	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Atstovai { get; set; }
	}

	/// <summary>
	/// Entity view.
	/// </summary>
	public MokinysM Mokinys { get; set; } = new MokinysM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}
/*public class ModelisCE
{
	/// <summary>
	/// Entity data
	/// </summary>
	public class ModelM
	{
		[Display(Name="Id")]
		public int Id { get; set; }

		[Display(Name="Pavadinimas")]
		[MaxLength(20)]
		[Required]
		public string Pavadinimas { get; set; }

		[Display(Name="Markė")]
		[Required]
		public int FkMarke { get; set; }
	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Markes { get; set; }
	}

	/// <summary>
	/// Entity view.
	/// </summary>
	public ModelM Model { get; set; } = new ModelM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}*/

