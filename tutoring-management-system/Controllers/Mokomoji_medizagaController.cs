namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.Paslauga;


/// <summary>
/// Controller for working with 'Paslauga' entity.
/// </summary>
public class Mokomoji_medizagaController : ControllerBase
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		var medizagos = Repositories.Mokomoji_medizagaRepo.List();
		return View(medizagos);
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var medizaga = new Mokomoji_medizaga();

		
		return View(medizaga);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="paslaugaEvm">Entity view model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(Models.Paslauga.Mokomoji_medizaga medizaga)
	{
		
		if (ModelState.IsValid)
		{
            
            Repositories.Mokomoji_medizagaRepo.Insert(medizaga);

			
			return RedirectToAction("Index");
		}

		
		return View(medizaga);
	}

	
	public ActionResult Edit(int id)
	{
		var medizaga = Repositories.Mokomoji_medizagaRepo.Find(id);
		return View(medizaga);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>
	/// <param name="medizaga">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, Models.Paslauga.Mokomoji_medizaga medizaga)
	{
		
		if (ModelState.IsValid)
		{
            
            Repositories.Mokomoji_medizagaRepo.Update(medizaga);

			
			return RedirectToAction("Index");
		}

		
		return View(medizaga);
	}

	/// <summary>
	/// This is invoked when deletion form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var medizaga = Repositories.Mokomoji_medizagaRepo.Find(id);
		return View(medizaga);
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
            // Try deleting the entity
            Repositories.Mokomoji_medizagaRepo.Delete(id);

			// Deletion success, redirect to list form
			return RedirectToAction("Index");
		}
		catch (MySql.Data.MySqlClient.MySqlException)
		{
			// Entity in use (foreign key constraint failed), deletion not permitted
			ViewData["deletionNotPermitted"] = true;

			var medizaga = Repositories.Mokomoji_medizagaRepo.Find(id);
			return View("Delete", medizaga);
		}
	}
}
/*public class PaslaugaController : ControllerBase
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return base.View(Repositories.Mokomoji_medizaga.List());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var paslaugaEvm = new PaslaugaCE();
		return View(paslaugaEvm);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="paslaugaEvm">Entity view model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(int? save, int? add, int? remove, PaslaugaCE paslaugaEvm)
	{
		//addition of new 'PaslauguKainos' record was requested?
		if( add != null )
		{
			//add entry for the new record
			var pk =
				new PaslaugosKaina {
					InListId =
						paslaugaEvm.Kainos.Count > 0 ?
						paslaugaEvm.Kainos.Max(it => it.InListId) + 1 :
						0,
					IsReadonly = false,

					GaliojaNuo = DateTime.Now
				};
			paslaugaEvm.Kainos.Add(pk);

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			return View(paslaugaEvm);
		}

		//removal of existing 'PaslauguKainos' record was requested?
		if( remove != null )
		{
			//filter out 'PaslauguKainos' record having in-list-id the same as the given one
			paslaugaEvm.Kainos =
				paslaugaEvm
					.Kainos
					.Where(it => it.InListId != remove.Value)
					.ToList();

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			return View(paslaugaEvm);
		}

		//save of the form data was requested?
		if( save != null )
		{
			//check for duplicate 'GaliojaNuo' fields in 'PaslaugosKainos' list
			for( var index = 0; index < paslaugaEvm.Kainos.Count; index++ )
			{
				//find all entries that are not current one and have matching 'GaliojaNuo' field
				var matches = 
					paslaugaEvm.Kainos.Where((other, otherIndex) => {
						return 
							other.GaliojaNuo == paslaugaEvm.Kainos[index].GaliojaNuo &&
							otherIndex != index;
					})
					.ToList();

				//entries found? mark current field as invalid by adding error message to model state
				if( matches.Count > 0 )
					ModelState.AddModelError($"Kainos[{index}].GaliojaNuo", "Field value already exists");
			}
			
			//form field validation passed?
			if( ModelState.IsValid )
			{
				//insert 'Paslauga'
				paslaugaEvm.Paslauga.Id = Repositories.Mokomoji_medizaga.Insert(paslaugaEvm.Paslauga);

				//insert related 'PaslaugosKainos'
				foreach( var kainaInForm in paslaugaEvm.Kainos )
				{	
					kainaInForm.FkPaslauga = paslaugaEvm.Paslauga.Id;					
					PaslaugosKainaRepo.Insert(kainaInForm);
				}

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}
			//form field validation failed, go back to the form
			{
				return View(paslaugaEvm);
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
		var paslaugaEvm = new PaslaugaCE();

		paslaugaEvm.Paslauga = Repositories.Mokomoji_medizaga.Find(id);
		paslaugaEvm.Kainos = PaslaugosKainaRepo.LoadForPaslauga(id);

		return View(paslaugaEvm);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="paslaugaEvm">Entity view model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, int? save, int? add, int? remove, PaslaugaCE paslaugaEvm)
	{
		//addition of new 'PaslauguKainos' record was requested?
		if( add != null )
		{
			//add entry for the new record
			var pk =
				new PaslaugosKaina {
					InListId =
						paslaugaEvm.Kainos.Count > 0 ?
						paslaugaEvm.Kainos.Max(it => it.InListId) + 1 :
						0,
					IsReadonly = false,

					FkPaslauga = paslaugaEvm.Paslauga.Id,
					GaliojaNuo = DateTime.Now
				};
			paslaugaEvm.Kainos.Add(pk);

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			return View(paslaugaEvm);
		}

		//removal of existing 'PaslauguKainos' record was requested?
		if( remove != null )
		{
			//filter out 'PaslauguKainos' record having in-list-id the same as the given one
			paslaugaEvm.Kainos =
				paslaugaEvm
					.Kainos
					.Where(it => it.InListId != remove.Value)
					.ToList();

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			return View(paslaugaEvm);
		}

		//save of the form data was requested?
		if( save != null )
		{
			//check for duplicate 'GaliojaNuo' fields in 'PaslaugosKainos' list
			for( var index = 0; index < paslaugaEvm.Kainos.Count; index++ )
			{
				//find all entries that are not current one and have matching 'GaliojaNuo' field
				var matches = 
					paslaugaEvm.Kainos.Where((other, otherIndex) => {
						return 
							other.GaliojaNuo == paslaugaEvm.Kainos[index].GaliojaNuo &&
							otherIndex != index;
					})
					.ToList();

				//entries found? mark current field as invalid by adding error message to model state
				if( matches.Count > 0 )
					ModelState.AddModelError($"Kainos[{index}].GaliojaNuo", "Field value already exists");
			}
			
			//form field validation passed?
			if( ModelState.IsValid )
			{
                //update 'Paslauga'
                Repositories.Mokomoji_medizaga.Update(paslaugaEvm.Paslauga);

				//update related 'PaslaugosKainos'
				{
					//load related 'PaslaugosKainos' from DB to have most up to date data
					var kainosInDb = PaslaugosKainaRepo.LoadForPaslauga(paslaugaEvm.Paslauga.Id);

					//delete 'PaslaugosKainos' that are not present in form (if deletable)
					foreach( var kainaInDb in kainosInDb )
					{
						var delete =
							paslaugaEvm.Kainos.Find(it => it.GaliojaNuo == kainaInDb.GaliojaNuo) == null && 
							!kainaInDb.IsReadonly;
						if( delete )
							PaslaugosKainaRepo.Delete(kainaInDb.FkPaslauga, kainaInDb.GaliojaNuo);
					}

					//insert 'PaslaugosKainos' that are not present in DB
					foreach( var kainaInForm in paslaugaEvm.Kainos )
					{
						if( kainosInDb.Find(it => it.GaliojaNuo == kainaInForm.GaliojaNuo) == null ) 
							PaslaugosKainaRepo.Insert(kainaInForm);
					}

					//update non-readonly 'PaslaugosKainos' in DB (deleted entities will simply result in no-action as far as SQL is concerned)
					foreach( var kainaInDb in kainosInDb )
					{
						var update = paslaugaEvm.Kainos.Find(it => it.GaliojaNuo == kainaInDb.GaliojaNuo);
						if( update != null && !kainaInDb.IsReadonly )
							PaslaugosKainaRepo.Update(update);
					}						
				}

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}
			//form field validation failed, go back to the form
			{
				return View(paslaugaEvm);
			}
		}

		//should not reach here
		throw new Exception("Should not reach here.");
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var paslaugaEvm = Repositories.Mokomoji_medizaga.Find(id);
		return View(paslaugaEvm);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		//load related 'PaslaugosKainos'
		var kainos = PaslaugosKainaRepo.LoadForPaslauga(id);

		//find if 'Paslauga' is in use (there is a related 'PaslaugosKaina' that is in use)
		var isInUse = kainos.Find(it => it.IsReadonly) != null;

		//not in use? delete
		if( !isInUse )
		{
			//delete dependant entities
			foreach( var kaina in kainos )
				PaslaugosKainaRepo.Delete(kaina.FkPaslauga, kaina.GaliojaNuo);

            //delete the entity 
            Repositories.Mokomoji_medizaga.Delete(id);

			//redired to list form
			return RedirectToAction("Index");
		}
		//in use, deletion not permitted
		else
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var paslaugaEvm = Repositories.Mokomoji_medizaga.Find(id);
			return View("Delete", paslaugaEvm);
		}
	}
}
*/