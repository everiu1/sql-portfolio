namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Database operations related to 'Klientas' entity.
/// </summary>
public class KorepetitoriusRepo : RepoBase
{
	public static List<Korepetitorius> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}Korepetitoriai` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Korepetitorius>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Vardas = dre.From<string>("Vardas"); 
				t.Pavarde = dre.From<string>("Pavarde");
				t.El_pastas = dre.From<string>("El_pastas");
				t.Tel_numeris = dre.From<string>("Tel_numeris");
				t.Valandos_per_savaite = dre.From<int?>("Valandos_per_savaite");
				t.Aprasymas = dre.From<string>("Aprasymas");
				t.Mokymo_patirtis = dre.From<int?>("Mokymo_patirtis");
			});

		return result;
	}
	/*public static List<Korepetitorius> List()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}klientai` ORDER BY asmens_kodas ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Korepetitorius>(drc, (dre, t) => {
				t.AsmensKodas = dre.From<string>("asmens_kodas");
				t.Vardas = dre.From<string>("vardas");
				t.Pavarde = dre.From<string>("pavarde");
				t.GimimoData = dre.From<DateTime>("gimimo_data");
				t.Telefonas = dre.From<string>("telefonas");
				t.Epastas = dre.From<string>("epastas");
			});

		return result;
	}*/

	public static Korepetitorius Find(int id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}Korepetitoriai` WHERE id=?id";

		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		if( drc.Count > 0 )
		{
			var result = 
				Sql.MapOne<Korepetitorius>(drc, (dre, t) => {
					t.Id = dre.From<int>("id");
					t.Vardas = dre.From<string>("Vardas");
					t.Pavarde = dre.From<string>("Pavarde");
					t.El_pastas = dre.From<string>("El_pastas");
					t.Tel_numeris = dre.From<string>("Tel_numeris");
					t.Valandos_per_savaite = dre.From<int?>("Valandos_per_savaite");
					t.Aprasymas = dre.From<string>("Aprasymas");
					t.Mokymo_patirtis = dre.From<int?>("Mokymo_patirtis");
				});

			return result;
		}

		return null;
	}
	/*public static Korepetitorius Find(string asmkodas)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}klientai` WHERE asmens_kodas=?asmkodas";

		var drc =
			Sql.Query(query, args => {
				args.Add("?asmkodas", asmkodas);
			});

		if( drc.Count > 0 )
		{
			var result = 
				Sql.MapOne<Korepetitorius>(drc, (dre, t) => {
					t.AsmensKodas = dre.From<string>("asmens_kodas");
					t.Vardas = dre.From<string>("vardas");
					t.Pavarde = dre.From<string>("pavarde");
					t.GimimoData = dre.From<DateTime>("gimimo_data");
					t.Telefonas = dre.From<string>("telefonas");
					t.Epastas = dre.From<string>("epastas");
				});

			return result;
		}

		return null;
	}*/

	public static void Insert(Korepetitorius korepetitorius)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}Korepetitoriai`
			(
				Vardas,
			Pavarde,
			El_pastas,
			Tel_numeris,
			Valandos_per_savaite,
			Aprasymas,
			Mokymo_patirtis
			)
			VALUES(
				?vardas,
			?pavarde,
			?el_pastas,
			?tel_numeris,
			?valandos,
			?aprasymas,
			?patirtis
			)";

		Sql.Insert(query, args => {
			args.Add("?vardas", korepetitorius.Vardas);
			args.Add("?pavarde", korepetitorius.Pavarde);
			args.Add("?el_pastas", korepetitorius.El_pastas);
			args.Add("?tel_numeris", korepetitorius.Tel_numeris);
			args.Add("?valandos", korepetitorius.Valandos_per_savaite);
			args.Add("?aprasymas", korepetitorius.Aprasymas);
			args.Add("?patirtis", korepetitorius.Mokymo_patirtis);
		});
	}
	/*public static void Insert(Korepetitorius klientas)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}klientai`
			(
				asmens_kodas,
				vardas,
				pavarde,
				gimimo_data,
				telefonas,
				epastas
			)
			VALUES(
				?asmkod,
				?vardas,
				?pavarde,
				?gimdata,
				?tel,
				?email
			)";

		Sql.Insert(query, args => {
			args.Add("?asmkod", klientas.AsmensKodas);
			args.Add("?vardas", klientas.Vardas);
			args.Add("?pavarde", klientas.Pavarde);
			args.Add("?gimdata", klientas.GimimoData);
			args.Add("?tel", klientas.Telefonas);
			args.Add("?email", klientas.Epastas);
		});
	}
*/
	public static void Update(Korepetitorius korepetitorius)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}korepetitoriai`
			SET
				Vardas=?vardas,
			Pavarde=?pavarde,
			El_pastas=?el_pastas,
			Tel_numeris=?tel_numeris,
			Valandos_per_savaite=?valandos,
			Aprasymas=?aprasymas,
			Mokymo_patirtis=?patirtis
			WHERE
				id=?id";

		Sql.Update(query, args => {
			args.Add("?vardas", korepetitorius.Vardas);
			args.Add("?pavarde", korepetitorius.Pavarde);
			args.Add("?el_pastas", korepetitorius.El_pastas);
			args.Add("?tel_numeris", korepetitorius.Tel_numeris);
			args.Add("?valandos", korepetitorius.Valandos_per_savaite);
			args.Add("?aprasymas", korepetitorius.Aprasymas);
			args.Add("?patirtis", korepetitorius.Mokymo_patirtis);

		
			args.Add("?id", korepetitorius.Id);
		});
	}

	/*public static void Update(Korepetitorius klientas)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}klientai`
			SET
				vardas=?vardas,
				pavarde=?pavarde,
				gimimo_data=?gimdata,
				telefonas=?tel,
				epastas=?email
			WHERE
				asmens_kodas=?asmkod";

		Sql.Update(query, args => {
			args.Add("?asmkod", klientas.AsmensKodas);
			args.Add("?vardas", klientas.Vardas);
			args.Add("?pavarde", klientas.Pavarde);
			args.Add("?gimdata", klientas.GimimoData);
			args.Add("?tel", klientas.Telefonas);
			args.Add("?email", klientas.Epastas);
		});
	}
*/
	public static void Delete(int id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}korepetitoriai` WHERE id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}
	/*public static void Delete(string id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}korepetitoriai` WHERE asmens_kodas=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}*/
}
