namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Paslauga;


/// <summary>
/// Database operations related to 'Paslauga' entity.
/// </summary>
public class Mokomoji_medizagaRepo : RepoBase
{
	
	public static List<Mokomoji_medizaga> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}Mokomosios_medizagos` ORDER BY id ASC";
		var drc = Sql.Query(query);

		
		var result =
			Sql.MapAll<Mokomoji_medizaga>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Dalykas = dre.From<string>("Dalykas");
				t.Pavadinimas = dre.From<string>("Pavadinimas");
				t.Klase = dre.From<string>("Klase");
				t.Tipas = dre.From<string>("Tipas");
				t.Dalis = dre.From<int?>("Dalis");
			});

		return result;
	}

	
	public static Mokomoji_medizaga Find(int id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}Mokomosios_medizagos` WHERE id=?id";
		var drc = Sql.Query(query, args => { args.Add("?id", id); });

		var result =
			Sql.MapOne<Mokomoji_medizaga>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Dalykas = dre.From<string>("Dalykas");
				t.Pavadinimas = dre.From<string>("Pavadinimas");
				t.Klase = dre.From<string>("Klase");
				t.Tipas = dre.From<string>("Tipas");
				t.Dalis = dre.From<int?>("Dalis");
			});

		return result;
	}

	
	public static void Update(Mokomoji_medizaga medizaga)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}Mokomosios_medizagos` 
            SET 
                Dalykas=?dalykas, 
                Pavadinimas=?pavadinimas, 
                Klase=?klase, 
                Tipas=?tipas, 
                Dalis=?dalis 
            WHERE id=?id";

		Sql.Update(query, args => {
			args.Add("?id", medizaga.Id);
			args.Add("?dalykas", medizaga.Dalykas);
			args.Add("?pavadinimas", medizaga.Pavadinimas);
			args.Add("?klase", medizaga.Klase);
			args.Add("?tipas", medizaga.Tipas);
			args.Add("?dalis", medizaga.Dalis);
		});
	}

	
	public static int Insert(Mokomoji_medizaga medizaga)
	{
		string query =
			$@"INSERT INTO `{Config.TblPrefix}Mokomosios_medizagos`
            (Dalykas, Pavadinimas, Klase, Tipas, Dalis)
            VALUES
            (?dalykas, ?pavadinimas, ?klase, ?tipas, ?dalis)";

		var id =
			Sql.Insert(query, args => {
				args.Add("?dalykas", medizaga.Dalykas);
				args.Add("?pavadinimas", medizaga.Pavadinimas);
				args.Add("?klase", medizaga.Klase);
				args.Add("?tipas", medizaga.Tipas);
				args.Add("?dalis", medizaga.Dalis);
			});

		return (int)id;
	}

	public static void Delete(int id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}Mokomosios_medizagos` WHERE id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}
}

/*public class Mokomoji_medizaga : RepoBase
{
	public static List<Mokomoji_medizaga> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}paslaugos` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Mokomoji_medizaga>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Aprasymas = dre.From<string>("aprasymas");
			});

		return result;
	}

	public static Mokomoji_medizaga Find(int id)
	{
		var query = 
			$@"SELECT 
				a.id,
				a.pavadinimas,
				a.aprasymas
			FROM `{Config.TblPrefix}paslaugos` a
			WHERE a.id=?id";

		var drc = 
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result = 
			Sql.MapOne<Mokomoji_medizaga>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Aprasymas = dre.From<string>("aprasymas");
			});

		return result;
	}

	public static void Update(Mokomoji_medizaga paslauga)
	{
		var query = 
			$@"UPDATE `{Config.TblPrefix}paslaugos` p 
			SET p.pavadinimas=?pavadinimas, p.aprasymas=?aprasymas 
			WHERE p.id=?id";

		Sql.Update(query, args => {
			args.Add("?id", paslauga.Id);
			args.Add("?pavadinimas", paslauga.Pavadinimas);
			args.Add("?aprasymas", paslauga.Aprasymas);
		});
	}

	public static int Insert(Mokomoji_medizaga paslauga)
	{
		string query = 
			$@"INSERT INTO `{Config.TblPrefix}paslaugos`
			(pavadinimas,aprasymas)
			VALUES
			(?pavadinimas,?aprasymas)";

		var id =
			(int)Sql.Insert(query, args => {
				args.Add("?pavadinimas", paslauga.Pavadinimas);
				args.Add("?aprasymas", paslauga.Aprasymas);
			});

		return (int)id;
	}

	public static void Delete(int id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}paslaugos` WHERE id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}
}*/
