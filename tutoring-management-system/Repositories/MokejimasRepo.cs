namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;


public class MokejimasRepo : RepoBase
{
	public static List<Mokejimas> List()
	{
		string query = $@"SELECT * FROM `{Config.TblPrefix}Mokejimai` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<Mokejimas>(drc, (dre, t) => {
				
				t.Id = dre.From<int>("id");
				t.Suma = dre.From<int?>("Suma");
				t.Paskirtis = dre.From<string>("Paskirtis");
				t.Data = dre.From<DateTime?>("Data");
				t.Saskaitos_numeris = dre.From<string>("Saskaitos_numeris");
				t.Fk_Mokinys = dre.From<int>("fk_Mokinys");
			});

		return result;
	}

	public static Mokejimas Find(int id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}Mokejimai` WHERE id=?id";
		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result =
			Sql.MapOne<Mokejimas>(drc, (dre, t) => {
			
				t.Id = dre.From<int>("id");
				t.Suma = dre.From<int?>("Suma");
				t.Paskirtis = dre.From<string>("Paskirtis");
				t.Data = dre.From<DateTime?>("Data");
				t.Saskaitos_numeris = dre.From<string>("Saskaitos_numeris");
				t.Fk_Mokinys = dre.From<int>("fk_Mokinys");
			});

		return result;
	}

	public static void Update(Mokejimas mokejimas)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}Mokejimai` 
			SET 
				Suma=?suma,
				Paskirtis=?paskirtis,
				Data=?data,
				Saskaitos_numeris=?saskaitos_numeris,
				fk_Mokinys=?fk_mokinys
			WHERE 
				id=?id";

		Sql.Update(query, args => {
			args.Add("?suma", mokejimas.Suma);
			args.Add("?paskirtis", mokejimas.Paskirtis);
			args.Add("?data", mokejimas.Data);
			args.Add("?saskaitos_numeris", mokejimas.Saskaitos_numeris);
			args.Add("?fk_mokinys", mokejimas.Fk_Mokinys);

			args.Add("?id", mokejimas.Id);
		});
	}

	public static void Insert(Mokejimas mokejimas)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}Mokejimai` 
			( Suma, Paskirtis, Data, Saskaitos_numeris, fk_Mokinys ) 
			VALUES 
			( ?suma, ?paskirtis, ?data, ?saskaitos_numeris, ?fk_mokinys )";

		Sql.Insert(query, args => {
			args.Add("?suma", mokejimas.Suma);
			args.Add("?paskirtis", mokejimas.Paskirtis);
			args.Add("?data", mokejimas.Data);
			args.Add("?saskaitos_numeris", mokejimas.Saskaitos_numeris);
			args.Add("?fk_mokinys", mokejimas.Fk_Mokinys);
		});
	}

	public static void Delete(int id)
	{			
		var query = $@"DELETE FROM `{Config.TblPrefix}Mokejimai` where id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});			
	}
}
/*public class MokejimasRepo : RepoBase
{
	public static List<Mokejimas> List()
	{
		string query = $@"SELECT * FROM `{Config.TblPrefix}markes` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Mokejimas>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
			});

		return result;
	}

	public static Mokejimas Find(int id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}markes` WHERE id=?id";
		var drc = 
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result = 
			Sql.MapOne<Mokejimas>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
			});

		return result;
	}

	public static void Update(Mokejimas marke)
	{			
		var query = 
			$@"UPDATE `{Config.TblPrefix}markes` 
			SET 
				pavadinimas=?pavadinimas 
			WHERE 
				id=?id";

		Sql.Update(query, args => {
			args.Add("?pavadinimas", marke.Pavadinimas);
			args.Add("?id", marke.Id);
		});							
	}

	public static void Insert(Mokejimas marke)
	{			
		var query = $@"INSERT INTO `{Config.TblPrefix}markes` ( pavadinimas ) VALUES ( ?pavadinimas )";
		Sql.Insert(query, args => {
			args.Add("?pavadinimas", marke.Pavadinimas);
		});
	}

	public static void Delete(int id)
	{			
		var query = $@"DELETE FROM `{Config.TblPrefix}markes` where id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});			
	}
}
*/
