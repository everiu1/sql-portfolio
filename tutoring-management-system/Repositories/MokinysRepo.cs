namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Modelis;
using static Org.Ktu.Isk.P175B602.Autonuoma.Models.Modelis.MokinysCE;


public class MokinysRepo : RepoBase
{
	public static List<MokinysL> List()
	{
		var query =
			$@"SELECT
				m.id,
			m.Vardas,
			m.Pavarde,
			m.Klasė,
			CONCAT(a.Vardas, ' ', a.Pavarde) AS atstovas
		FROM
			`{Config.TblPrefix}Mokiniai` m
			LEFT JOIN `{Config.TblPrefix}Mokinio_atstovai` a ON a.id = m.fk_Mokinio_atstovas
		ORDER BY m.Pavarde ASC, m.Vardas ASC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<MokinysL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Vardas = dre.From<string>("Vardas");
				t.Pavarde = dre.From<string>("Pavarde");
				t.Klase = dre.From<int?>("Klasė");
				t.Atstovas = dre.From<string>("atstovas");
			});

		return result;
	}
	/*public static List<MokinysL> List()
	{
		var query =
			$@"SELECT
				md.id,
				md.Vardas,
				md.Pavarde,
				md.Tel_numeris,
				md.El_pastas

			FROM
				`{Config.TblPrefix}Mokinio_atstovai` md
				LEFT JOIN `{Config.TblPrefix}markes` mark ON mark.id=md.fk_marke
			ORDER BY mark.pavadinimas ASC, md.id ASC";

		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<MokinysL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Marke = dre.From<string>("marke");
			});

		return result;
	}*/

	public static List<Mokinys> ListForMokinioAtstovas(int atstovasId)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}Mokiniai` WHERE fk_Mokinio_atstovas=?atstovasId ORDER BY id ASC";
		var drc =
			Sql.Query(query, args => {
				args.Add("?atstovasId", atstovasId);
			});

		var result =
			Sql.MapAll<Mokinys>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Vardas = dre.From<string>("Vardas");
				t.Pavarde = dre.From<string>("Pavarde");
				t.Klase = dre.From<int?>("Klasė");
				t.El_pastas = dre.From<string>("El_pastas");
				t.Tel_numeris = dre.From<string>("Tel_numeris");
				t.Registracijos_data = dre.From<DateTime?>("Registracijos_data");
				t.FkMokinioAtstovas = dre.From<int>("fk_Mokinio_atstovas");
			});

		return result;
	}
	/*public static List<Mokinys> ListForMarke(int markeId)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}modeliai` WHERE fk_marke=?markeId ORDER BY id ASC";
		var drc =
			Sql.Query(query, args => {
				args.Add("?markeId", markeId);
			});

		var result = 
			Sql.MapAll<Mokinys>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.FkMarke = dre.From<int>("fk_marke");
			});

		return result;
	}*/

	public static MokinysCE Find(int id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}Mokiniai` WHERE id=?id";
		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result =
			Sql.MapOne<MokinysCE>(drc, (dre, t) => {
				t.Mokinys.Id = dre.From<int>("id");
				t.Mokinys.Vardas = dre.From<string>("Vardas");
				t.Mokinys.Pavarde = dre.From<string>("Pavarde");
				t.Mokinys.Klase = dre.From<int?>("Klasė");
				t.Mokinys.El_pastas = dre.From<string>("El_pastas");
				t.Mokinys.Tel_numeris = dre.From<string>("Tel_numeris");
				t.Mokinys.Registracijos_data = dre.From<DateTime?>("Registracijos_data");
				t.Mokinys.FkMokinioAtstovas = dre.From<int>("fk_Mokinio_atstovas");
			});

		return result;
	}
	/*public static MokinysCE Find(int id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}modeliai` WHERE id=?id";
		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result = 
			Sql.MapOne<MokinysCE>(drc, (dre, t) => {
				t.Mokinys.Id = dre.From<int>("id");
				t.Mokinys.Pavadinimas = dre.From<string>("pavadinimas");
				t.Mokinys.FkMarke = dre.From<int>("fk_marke");
			});

		return result;
	}*/

	public static MokinysL FindForDeletion(int id)
	{
		var query =
			$@"SELECT
				m.id,
			m.Vardas,
			m.Pavarde,
			CONCAT(a.Vardas, ' ', a.Pavarde) AS atstovas
			FROM
				`{Config.TblPrefix}Mokiniai` m
				LEFT JOIN `{Config.TblPrefix}Mokinio_atstovai` a ON a.id = m.fk_Mokinio_atstovas
			WHERE
				m.id = ?id";

		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result =
			Sql.MapOne<MokinysL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Vardas = dre.From<string>("Vardas");
				t.Pavarde = dre.From<string>("Pavarde");
				t.Atstovas = dre.From<string>("atstovas");
			});

		return result;
	}
	/*public static MokinysL FindForDeletion(int id)
	{
		var query =
			$@"SELECT
				md.id,
				md.pavadinimas,
				mark.pavadinimas AS marke
			FROM
				`{Config.TblPrefix}modeliai` md
				LEFT JOIN `{Config.TblPrefix}markes` mark ON mark.id=md.fk_marke
			WHERE
				md.id = ?id";

		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result = 
			Sql.MapOne<MokinysL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Marke = dre.From<string>("marke");
			});

		return result;
	}*/

	public static void Update(MokinysCE mokinysCE)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}Mokiniai`
			SET
				Vardas = ?Vardas,
			Pavarde = ?Pavarde,
			Klasė = ?Klase,
			El_pastas = ?El_pastas,
			Tel_numeris = ?Tel_numeris,
			Registracijos_data = ?Registracijos_data,
			fk_Mokinio_atstovas = ?fk_Mokinio_atstovas
			WHERE
				id=?id
		";

		Sql.Update(query, args => {
			args.Add("?Vardas", mokinysCE.Mokinys.Vardas);
			args.Add("?Pavarde", mokinysCE.Mokinys.Pavarde);
			args.Add("?Klase", mokinysCE.Mokinys.Klase);
			args.Add("?El_pastas", mokinysCE.Mokinys.El_pastas);
			args.Add("?Tel_numeris", mokinysCE.Mokinys.Tel_numeris);
			args.Add("?Registracijos_data", mokinysCE.Mokinys.Registracijos_data);
			args.Add("?fk_Mokinio_atstovas", mokinysCE.Mokinys.FkMokinioAtstovas);
		});
	}
	/*public static void Update(MokinysCE modelisEvm)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}modeliai`
			SET
				pavadinimas=?pavadinimas,
				fk_marke=?marke
			WHERE
				id=?id";

		Sql.Update(query, args => {
			args.Add("?pavadinimas", modelisEvm.Mokinys.Pavadinimas);
			args.Add("?marke", modelisEvm.Mokinys.FkMarke);
			args.Add("?id", modelisEvm.Mokinys.Id);
		});
	}*/

	public static void Insert(MokinysCE mokinysEvm)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}Mokiniai`
			(
				Vardas,
			Pavarde,
			Klasė,
			El_pastas,
			Tel_numeris,
			Registracijos_data,
			fk_Mokinio_atstovas
			)
			VALUES(
				?Vardas,
			?Pavarde,
			?Klase,
			?El_pastas,
			?Tel_numeris,
			?Registracijos_data,
			?fk_Mokinio_atstovas
			)";

		Sql.Insert(query, args => {
			args.Add("?Vardas", mokinysEvm.Mokinys.Vardas);
			args.Add("?Pavarde", mokinysEvm.Mokinys.Pavarde);
			args.Add("?Klase", mokinysEvm.Mokinys.Klase);
			args.Add("?El_pastas", mokinysEvm.Mokinys.El_pastas);
			args.Add("?Tel_numeris", mokinysEvm.Mokinys.Tel_numeris);
			args.Add("?Registracijos_data", mokinysEvm.Mokinys.Registracijos_data);
			args.Add("?fk_Mokinio_atstovas", mokinysEvm.Mokinys.FkMokinioAtstovas);
		});
	}
	/*public static void Insert(MokinysCE modelisEvm)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}modeliai`
			(
				pavadinimas,
				fk_marke
			)
			VALUES(
				?pavadinimas,
				?marke
			)";

		Sql.Insert(query, args => {
			args.Add("?pavadinimas", modelisEvm.Mokinys.Pavadinimas);
			args.Add("?marke", modelisEvm.Mokinys.FkMarke);
		});
	}*/

	public static void Delete(int id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}Mokiniai` WHERE id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}
	/*public static void Delete(int id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}modeliai` WHERE id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}*/
}
