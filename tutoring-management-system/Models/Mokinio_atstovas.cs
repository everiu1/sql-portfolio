namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Automobilis;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Org.Ktu.Isk.P175B602.Autonuoma.Models.Automobilis.Mokinio_atstovasCE;


/// <summary>
/// 'Mokinio_atstovaL' in list form (senas auomobilis).
/// </summary>
public class Mokinio_atstovasL
{
	[Display(Name="Id")]
	public int Id { get; set; }

	[Display(Name="Vardas")]
	public string Vardas { get; set; }
    [Display(Name = "Pavarde")]
    public string Pavarde { get; set; }

    [Display(Name="Tel_numeris")]
	public string Tel_numeris { get; set; }

	[Display(Name="El_pastas")]
	public string El_pastas { get; set; }

}

/// <summary>
/// 'Mokinio_atstovai' in create and edit forms.
/// </summary>
public class Mokinio_atstovasCE
{
	/// <summary>
	/// Mokinio atstovas.
	/// </summary>
	public class Mokinio_atstovasM
	{
		[Display(Name = "Id")]
		[Required]
		public int Id { get; set; }

		[Display(Name = "Vardas")]
		[MaxLength(30)]
		[Required]
		public string Vardas { get; set; }

		[Display(Name = "Pavarde")]
		[MaxLength(30)]
		[Required]
		public string Pavarde { get; set; }

		[Display(Name = "Tel_numeris")]
		[MaxLength(13)]
		[Required]
		public string Tel_numeris { get; set; }

		[Display(Name = "El_pastas")]
		[MaxLength(50)]
		public string El_pastas { get; set; }

	}
    public Mokinio_atstovasM Mokinio_atstovas { get; set; } = new Mokinio_atstovasM();
}


/* <summary>
/// Select lists for making drop downs for choosing values of entity fields.
/// </summary>
public class ListsM
{
    public IList<SelectListItem> Busenos { get; set; }
    public IList<SelectListItem> Modeliai { get; set; }
    public IList<SelectListItem> PavaruDezes { get; set; }
    public IList<SelectListItem> KebuloTipai { get; set; }
    public IList<SelectListItem> DegaluTipai { get; set; }
    public IList<SelectListItem> Lagaminai { get; set; }

}


/// <summary>
/// Automobilis.
/// </summary>
public Mokinio_atstovasM Mokinio_Atstovas { get ; set; } = new Mokinio_atstovasM();

/// <summary>
/// Lists for drop down controls.
/// </summary>
public ListsM Lists { get; set; } = new ListsM();
}


/// <summary>
/// 'AutoBusena' enumerator in lists.
/// </summary>
public class AutoBusena
{
public int Id { get; set; }

public string Pavadinimas { get; set; }
}

/// <summary>
/// 'PavaruDeze' enumerator in lists.
/// </summary>
public class PavaruDeze
{
public int Id { get; set; }

public string Pavadinimas { get; set; }
}

/// <summary>
/// 'KebuloTipas' enumerator in lists.
/// </summary>
public class KebuloTipas
{
public int Id { get; set; }

public string Pavadinimas { get; set; }
}

/// <summary>
/// 'DegaluTipas' enumerator in lists.
/// </summary>
public class DegaluTipas
{
public int Id { get; set; }

public string Pavadinimas { get; set; }
}

/// <summary>
/// 'Lagaminas' enumerator in lists.
/// </summary>
public class Lagaminas
{
public int Id { get; set; }

public string Pavadinimas { get; set; }
}*/
