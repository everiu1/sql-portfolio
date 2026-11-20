namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;



/// <summary>
/// Database operations related to 'Aikstele' entity.
/// </summary>
public class TvarkarastisRepo : RepoBase
{
	public static List<Tvarkarastis> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}Tvarkarasciai` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<Tvarkarastis>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Data = dre.From<DateTime>("Data"); 
				t.Fk_Mokinys = dre.From<int>("fk_Mokinys");
			});

		return result;
	}
	public static Tvarkarastis Find(int id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}Tvarkarasciai` WHERE id=?id";
		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result =
			Sql.MapOne<Tvarkarastis>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Data = dre.From<DateTime>("Data");
				t.Fk_Mokinys = dre.From<int>("fk_Mokinys");
			});

		return result;
	}

	public static void Update(Tvarkarastis tvarkarastis)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}Tvarkarasciai` 
			SET 
				Data=?data,
				fk_Mokinys=?fk_mokinys
			WHERE 
				id=?id";

		Sql.Update(query, args => {
			args.Add("?data", tvarkarastis.Data);
			args.Add("?fk_mokinys", tvarkarastis.Fk_Mokinys);

			args.Add("?id", tvarkarastis.Id);
		});
	}

	public static void Insert(Tvarkarastis tvarkarastis)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}Tvarkarasciai` 
			( Data, fk_Mokinys ) 
			VALUES 
			( ?data, ?fk_mokinys )";

		Sql.Insert(query, args => {
			args.Add("?data", tvarkarastis.Data);
			args.Add("?fk_mokinys", tvarkarastis.Fk_Mokinys);
		});
	}

	public static void Delete(int id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}Tvarkarasciai` where id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}
}
