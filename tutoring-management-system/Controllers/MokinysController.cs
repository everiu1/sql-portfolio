namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.Modelis;


/// <summary>
/// Controller for working with 'Modelis' entity.
/// </summary>
public class MokinysController : ControllerBase
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		var mokiniai = MokinysRepo.List();
		return View(mokiniai);
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var mokinysEvm = new MokinysCE();
		PopulateSelections(mokinysEvm);
		return View(mokinysEvm);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="modelisCE">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(MokinysCE mokinysCE)
	{
		
		if( ModelState.IsValid )
		{
			MokinysRepo.Insert(mokinysCE);

			
			return RedirectToAction("Index");
		}

		
		PopulateSelections(mokinysCE);
		return View(mokinysCE);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var mokinysCE = MokinysRepo.Find(id);
		PopulateSelections(mokinysCE);

		return View(mokinysCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>
	/// <param name="autoEvm">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, MokinysCE mokinysCE)
	{
		//form field validation passed?
		if( ModelState.IsValid )
		{
			MokinysRepo.Update(mokinysCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(mokinysCE);
		return View(mokinysCE);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var mokinysLvm = MokinysRepo.FindForDeletion(id);
		return View(mokinysLvm);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		
		try
		{
			MokinysRepo.Delete(id);
			return RedirectToAction("Index");
		}
		
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			
			ViewData["deletionNotPermitted"] = true;

			var mokinysLvm = MokinysRepo.FindForDeletion(id);

			return View("Delete", mokinysLvm);
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="mokinysEvm">'Automobilis' view model to append to.</param>
	public void PopulateSelections(MokinysCE mokinysEvm)
	{
		
		var atstovai = Mokinio_atstovasRepo.ListMokinio_atstovas();

		
		mokinysEvm.Lists.Atstovai =
			atstovai.Select(it => {
				return
					new SelectListItem() {
						Value = Convert.ToString(it.Id),
						Text = $"{it.Vardas} {it.Pavarde}"
					};
			})
			.ToList();
	}
}
/*public class MokinysController : ControllerBase
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		var modeliai = MokinysRepo.List();
		return View(modeliai);
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var modelisEvm = new MokinysCE();
		PopulateSelections(modelisEvm);
		return View(modelisEvm);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="modelisCE">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(MokinysCE modelisCE)
	{
		//form field validation passed?
		if( ModelState.IsValid )
		{
			MokinysRepo.Insert(modelisCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(modelisCE);
		return View(modelisCE);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var modelisCE = MokinysRepo.Find(id);
		PopulateSelections(modelisCE);

		return View(modelisCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>
	/// <param name="autoEvm">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, MokinysCE modelisCE)
	{
		//form field validation passed?
		if( ModelState.IsValid )
		{
			MokinysRepo.Update(modelisCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(modelisCE);
		return View(modelisCE);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var modelisLvm = MokinysRepo.FindForDeletion(id);
		return View(modelisLvm);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		//try deleting, this will fail if foreign key constraint fails
		try
		{
			MokinysRepo.Delete(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var modelisLvm = MokinysRepo.FindForDeletion(id);

			return View("Delete", modelisLvm);
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="modelisEvm">'Automobilis' view model to append to.</param>
	public void PopulateSelections(MokinysCE modelisEvm)
	{
		//load entities for the select lists
		var markes = MarkeRepo.List();

		//build select lists
		modelisEvm.Lists.Atstovai =
			markes.Select(it => {
				return
					new SelectListItem() {
						Value = Convert.ToString(it.Id),
						Text = it.Pavadinimas
					};
			})
			.ToList();
	}
}*/
