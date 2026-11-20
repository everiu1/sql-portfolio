using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using System.Security.Policy;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.SutartisF2;


/// <summary>
/// Controller for working with 'Sutartis' entity. Implementation of F2 version.
/// </summary>
public class PamokaF2Controller : ControllerBase
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(PamokaF2Repo.ListPamoka());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in a browser.
	/// </summary>
	/// <returns>Entity creation form.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var pamokaCE = new PamokaCE();
		PopulateLists(pamokaCE);

		return View(pamokaCE);


	}


	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="pamokaCE">Entity view model filled with latest data.</param>
	/// <returns>Returns creation from view or redirets back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(int? save, int? add, int? remove, PamokaCE pamokaCE)
	{
		
		
		if (add != null)
		{
			
			var pm =
				new PamokaCE.PamokosMedziagaM
				{
					InListId =
						pamokaCE.PamokosMedziagos.Count > 0 ?
						pamokaCE.PamokosMedziagos.Max(it => it.InListId) + 1 :
						0,
					Paskirtis = "",
					NaudojimosiTrukme = null,
					Pastabos = ""
				};
			pamokaCE.PamokosMedziagos.Add(pm);

			
			ModelState.Clear();

			PopulateLists(pamokaCE);
			return View(pamokaCE);
		}

		
		if (remove != null)
		{
			
			pamokaCE.PamokosMedziagos =
					pamokaCE
						.PamokosMedziagos
						.Where(it => it.InListId != remove.Value)
						.ToList();

			ModelState.Clear();
			PopulateLists(pamokaCE);
			return View(pamokaCE);
		}

		
		if (save != null)
		{
			
			if (ModelState.IsValid)
			{
				
				pamokaCE.Pamoka.Id = PamokaF2Repo.InsertPamoka(pamokaCE);

				
				foreach (var pmVm in pamokaCE.PamokosMedziagos)
					PamokaF2Repo.InsertPamokosMedziaga(pamokaCE.Pamoka.Id, pmVm);

				
				return RedirectToAction("Index");
			}
			
			else
			{
				PopulateLists(pamokaCE);
				return View(pamokaCE);
			}
		}

	
		throw new Exception("Should not reach here.");
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var pamokaCE = PamokaF2Repo.FindPamokaCE(id);

		pamokaCE.PamokosMedziagos = PamokaF2Repo.ListPamokosMedziaga(id);
		PopulateLists(pamokaCE);

		return View(pamokaCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="pamokaCE">Entity view model filled with latest data.</param>
	/// <returns>Returns editing from view or redired back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, int? save, int? add, int? remove, PamokaCE pamokaCE)
	{
		if (add != null)
		{
			
			var pm =
				new PamokaCE.PamokosMedziagaM
				{
					InListId =
						pamokaCE.PamokosMedziagos.Count > 0 ?
						pamokaCE.PamokosMedziagos.Max(it => it.InListId) + 1 :
						0,
					Paskirtis = "",
					NaudojimosiTrukme = null,
					Pastabos = ""
				};
			pamokaCE.PamokosMedziagos.Add(pm);

			ModelState.Clear();
			PopulateLists(pamokaCE);
			return View(pamokaCE);
		}

		
		if (remove != null)
		{
			
			pamokaCE.PamokosMedziagos =
				pamokaCE
					.PamokosMedziagos
					.Where(it => it.InListId != remove.Value)
					.ToList();

			ModelState.Clear();
			PopulateLists(pamokaCE);
			return View(pamokaCE);
		}

		
		if (save != null)
		{
			
			if (ModelState.IsValid)
			{
				
				PamokaF2Repo.UpdatePamoka(pamokaCE);

				
				PamokaF2Repo.DeletePamokosMedziagasForPamoka(pamokaCE.Pamoka.Id);

				
				foreach (var pmVm in pamokaCE.PamokosMedziagos)
					PamokaF2Repo.InsertPamokosMedziaga(pamokaCE.Pamoka.Id, pmVm);

				return RedirectToAction("Index");
			}
			else
			{
				PopulateLists(pamokaCE);
				return View(pamokaCE);
			}
		}

		
		throw new Exception("Should not reach here.");
	}

	/// <summary>
	/// This is invoked when deletion form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var pamokaCE = PamokaF2Repo.FindPamokaCE(id);
		return View(pamokaCE);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		try
		{
			
			PamokaF2Repo.DeletePamokosMedziagasForPamoka(id);

			
			PamokaF2Repo.DeletePamoka(id);

			
			return RedirectToAction("Index");
		}
		catch (MySql.Data.MySqlClient.MySqlException)
		{
			
			ViewData["deletionNotPermitted"] = true;

			var pamokaCE = PamokaF2Repo.FindPamokaCE(id);
			return View("Delete", pamokaCE);
		}
	}


	private void PopulateLists(PamokaCE pamokaCE)
	{
		
		var mokiniai = MokinysRepo.List();
		var korepetitoriai = KorepetitoriusRepo.List();
		var mokejimai = MokejimasRepo.List(); 
		var tvarkarastis = TvarkarastisRepo.List();
		var mokomosiosMedziagos = Mokomoji_medizagaRepo.List(); 


		pamokaCE.Lists.Mokiniai = mokiniai.Select(it => new SelectListItem
		{
			Value = Convert.ToString(it.Id),
			Text = $"{it.Vardas} {it.Pavarde}"
		}).ToList();

		
		pamokaCE.Lists.Korepetitoriai = korepetitoriai.Select(it => new SelectListItem
		{
			Value = Convert.ToString(it.Id),
			Text = $"{it.Vardas} {it.Pavarde}"
		}).ToList();

		
		pamokaCE.Lists.Mokejimai = mokejimai.Select(it => new SelectListItem
		{
			Value = Convert.ToString(it.Id),
			
			Text = $"{it.Suma} EUR ({it.Paskirtis})"
		}).ToList();

		
		pamokaCE.Lists.Tvarkarastis = tvarkarastis.Select(it => new SelectListItem
		{
			Value = Convert.ToString(it.Id),
			
			Text = it.Data.ToString("yyyy-MM-dd")
		}).ToList();

	
		pamokaCE.Lists.MokomosiosMedziagos = mokomosiosMedziagos.Select(it => new SelectListItem
		{
			Value = Convert.ToString(it.Id),
			Text = it.Pavadinimas
		}).ToList();
	}
}
/*/// <summary>
/// Controller for working with 'Sutartis' entity. Implementation of F2 version.
/// </summary>
public class SutartisF2Controller : ControllerBase
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(PamokaF2Repo.ListPamoka());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in a browser.
	/// </summary>
	/// <returns>Entity creation form.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var sutCE = new PamokaCE();

		sutCE.Sutartis.SutartiesData = DateTime.Now;
		sutCE.Sutartis.NuomosDataLaikas = DateTime.Now;
		sutCE.Sutartis.PlanuojamaGrDataLaikas = DateTime.Now;
		
		PopulateLists(sutCE);

		return View(sutCE);
	}


	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="sutCE">Entity view model filled with latest data.</param>
	/// <returns>Returns creation from view or redirets back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(int? save, int? add, int? remove, PamokaCE sutCE)
	{
		//addition of new 'UzsakytosPaslaugos' record was requested?
		if( add != null )
		{
			//add entry for the new record
			var up =
				new PamokaCE.PamokosMedziagaM {
					InListId =
						sutCE.PriskirtosMedziagos.Count > 0 ?
						sutCE.PriskirtosMedziagos.Max(it => it.InListId) + 1 :
						0,

					Paslauga = null,
					Kiekis = 0,
					Kaina = 0
				};
			sutCE.PriskirtosMedziagos.Add(up);

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateLists(sutCE);
			return View(sutCE);
		}

		//removal of existing 'UzsakytosPaslaugos' record was requested?
		if( remove != null )
		{
			//filter out 'UzsakytosPaslaugos' record having in-list-id the same as the given one
			sutCE.PriskirtosMedziagos =
				sutCE
					.PriskirtosMedziagos
					.Where(it => it.InListId != remove.Value)
					.ToList();

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateLists(sutCE);
			return View(sutCE);
		}

		//save of the form data was requested?
		if( save != null )
		{
			//check for attemps to create duplicate 'UzsakytaPaslauga'records
			for( var i = 0; i < sutCE.PriskirtosMedziagos.Count-1; i ++ )
			{
				var refUp = sutCE.PriskirtosMedziagos[i];

				for( var j = i+1; j < sutCE.PriskirtosMedziagos.Count; j++ )
				{
					var testUp = sutCE.PriskirtosMedziagos[j];
					
					if( testUp.Paslauga == refUp.Paslauga )
						ModelState.AddModelError($"UzsakytosPaslaugos[{j}].Paslauga", "Duplicate of another added service.");
				}
			}

			//form field validation passed?
			if( ModelState.IsValid )
			{
				//create new 'Sutartis'
				sutCE.Sutartis.Nr = PamokaF2Repo.InsertPamoka(sutCE);

				//create new 'UzsakytosPaslaugos' records
				foreach( var upVm in sutCE.PriskirtosMedziagos )
					PamokaF2Repo.InsertUzsakytaPaslauga(sutCE.Sutartis.Nr, upVm);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}
			//form field validation failed, go back to the form
			else
			{
				PopulateLists(sutCE);
				return View(sutCE);
			}
		}

		//should not reach here
		throw new Exception("Should not reach here.");
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var sutCE = PamokaF2Repo.FindPamokaCE(id);
		
		sutCE.PriskirtosMedziagos = PamokaF2Repo.ListUzsakytaPaslauga(id);			
		PopulateLists(sutCE);

		return View(sutCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="sutCE">Entity view model filled with latest data.</param>
	/// <returns>Returns editing from view or redired back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, int? save, int? add, int? remove, PamokaCE sutCE)
	{
		//addition of new 'UzsakytosPaslaugos' record was requested?
		if( add != null )
		{
			//add entry for the new record
			var up =
				new PamokaCE.PamokosMedziagaM {
					InListId =
						sutCE.PriskirtosMedziagos.Count > 0 ?
						sutCE.PriskirtosMedziagos.Max(it => it.InListId) + 1 :
						0,

					Paslauga = null,
					Kiekis = 0,
					Kaina = 0
				};
			sutCE.PriskirtosMedziagos.Add(up);

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateLists(sutCE);
			return View(sutCE);
		}

		//removal of existing 'UzsakytosPaslaugos' record was requested?
		if( remove != null )
		{
			//filter out 'UzsakytosPaslaugos' record having in-list-id the same as the given one
			sutCE.PriskirtosMedziagos =
				sutCE
					.PriskirtosMedziagos
					.Where(it => it.InListId != remove.Value)
					.ToList();

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateLists(sutCE);
			return View(sutCE);
		}

		//save of the form data was requested?
		if( save != null )
		{
			//check for attemps to create duplicate 'UzsakytaPaslauga'records
			for( var i = 0; i < sutCE.PriskirtosMedziagos.Count-1; i ++ )
			{
				var refUp = sutCE.PriskirtosMedziagos[i];

				for( var j = i+1; j < sutCE.PriskirtosMedziagos.Count; j++ )
				{
					var testUp = sutCE.PriskirtosMedziagos[j];
					
					if( testUp.Paslauga == refUp.Paslauga )
						ModelState.AddModelError($"UzsakytosPaslaugos[{j}].Paslauga", "Duplicate of another added service.");
				}
			}

			//form field validation passed?
			if( ModelState.IsValid )
			{
				//update 'Sutartis'
				PamokaF2Repo.UpdatePamoka(sutCE);

				//delete all old 'UzsakytosPaslaugos' records
				PamokaF2Repo.DeleteUzsakytaPaslaugaForSutartis(sutCE.Sutartis.Nr);

				//create new 'UzsakytosPaslaugos' records
				foreach( var upVm in sutCE.PriskirtosMedziagos )
					PamokaF2Repo.InsertUzsakytaPaslauga(sutCE.Sutartis.Nr, upVm);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}
			//form field validation failed, go back to the form
			else
			{
				PopulateLists(sutCE);
				return View(sutCE);
			}
		}

		//should not reach here
		throw new Exception("Should not reach here.");
	}

	/// <summary>
	/// This is invoked when deletion form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var sutCE = PamokaF2Repo.FindPamokaCE(id);
		return View(sutCE);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		//load 'Sutartis'
		var sutCE = PamokaF2Repo.FindPamokaCE(id);

		//'Sutartis' is in the state where deletion is permitted?
		if( sutCE.Sutartis.FkBusena == 1 || sutCE.Sutartis.FkBusena == 3 )
		{
			//delete the entity
			PamokaF2Repo.DeleteUzsakytaPaslaugaForSutartis(id);
			PamokaF2Repo.DeletePamoka(id);

			//redired to list form
			return RedirectToAction("Index");
		}
		//'Sutartis' is in state where deletion is not permitted
		else
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;
			return View("Delete", sutCE);
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="sutCE">'Sutartis' view model to append to.</param>
	private void PopulateLists(PamokaCE sutCE)
	{
		//load entities for the select lists
		var automobiliai = Mokinio_atstovasRepo.ListMokinio_atstovas();
		var busenos = PamokaF2Repo.ListSutartiesBusena();
		var darbuotojai = DarbuotojasRepo.List();
		var klientai = KorepetitoriusRepo.List();
		var aiksteles = AiksteleRepo.List();

		//build select lists
		/*sutCE.Lists.Automobiliai =
			automobiliai
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.Id),
							Text = $"{it.ValstybinisNr} - {it.Marke} {it.Modelis}"
						};
				})
				.ToList();
		
sutCE.Lists.Busenos =
			busenos
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.Id),
							Text = it.Name
						};
				})
				.ToList();

sutCE.Lists.Darbuotojai =
	darbuotojai
		.Select(it =>
		{
			return
				new SelectListItem
				{
					Value = it.Tabelis,
					Text = $"{it.Vardas} {it.Pavarde}"
				};
		})
		.ToList();

sutCE.Lists.Klientai =
klientai
		.Select(it =>
		{
			return
				new SelectListItem
				{
					Value = it.AsmensKodas,
					Text = $"{it.Vardas} {it.Pavarde}"
				};
		})
		.ToList();

sutCE.Lists.Vietos =
	aiksteles
		.Select(it =>
		{
			return
				new SelectListItem
				{
					Value = Convert.ToString(it.Id),
					Text = it.Pavadinimas
				};
		})
		.ToList();

//build select list for 'UzsakytosPaslaugos'
{
	//initialize the destination list
	sutCE.Lists.Paslaugos = new List<SelectListItem>();

	//load 'Paslaugos' to use for item groups
	var paslaugos = PaslaugaRepo.List();

	//create select list items from 'PaslauguKainos' related to each 'Paslaugos'
	foreach (var paslauga in paslaugos)
	{
		//create list item group for current 'Paslaugos' entity
		var itemGrp = new SelectListGroup() { Name = paslauga.Pavadinimas };

		//load related 'PaslauguKaina' entities
		var kainos = PaslaugosKainaRepo.LoadForPaslauga(paslauga.Id);

		//build list items for the group
		foreach (var kaina in kainos)
		{
			var sle =
				new SelectListItem
				{
					Value =
						//we use JSON here to make serialization/deserializaton of composite key more convenient
						JsonConvert.SerializeObject(new
						{
							FkPaslauga = paslauga.Id,
							GaliojaNuo = kaina.GaliojaNuo
						}),
					Text = $"{paslauga.Pavadinimas} {kaina.Kaina} EUR ({kaina.GaliojaNuo.ToString("yyyy-MM-dd")})",
					Group = itemGrp
				};
			sutCE.Lists.Paslaugos.Add(sle);
		}
	}
}
	}
}*/
