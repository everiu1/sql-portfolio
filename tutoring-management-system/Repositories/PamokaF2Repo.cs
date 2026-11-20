namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using Newtonsoft.Json;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.SutartisF2;


/// <summary>
/// Database operations related to 'Sutartis' entity.
/// </summary>
public class PamokaF2Repo : RepoBase
{
	public static List<PamokaL> ListPamoka()
	{
		var query =
			$@"SELECT
				p.id,
				p.Dalykas,
				p.Trukme,
				CONCAT(k.Vardas, ' ', k.Pavarde) as korepetitorius,
				CONCAT(m.Vardas, ' ', m.Pavarde) as mokinys,
t.Data as tvarkarastis_data,
            CONCAT(mok.Suma, ' EUR (', mok.Paskirtis, ')') as mokejimas_info,

(SELECT COUNT(*) FROM `{Config.TblPrefix}Pamokos_medziagos` pm WHERE pm.fk_Pamoka = p.id) as medziagu_kiekis
			FROM
				`{Config.TblPrefix}Pamokos` p
				LEFT JOIN `{Config.TblPrefix}Korepetitoriai` k ON p.fk_Korepetitorius = k.id
				LEFT JOIN `{Config.TblPrefix}Mokiniai` m ON p.fk_Mokinys = m.id
LEFT JOIN `{Config.TblPrefix}Tvarkarasciai` t ON p.fk_Tvarkarastis = t.id
            LEFT JOIN `{Config.TblPrefix}Mokejimai` mok ON p.fk_Mokejimas = mok.id
			ORDER BY p.id DESC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<PamokaL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Dalykas = dre.From<string>("Dalykas");
				t.Trukme = dre.From<int?>("Trukme");
				t.Korepetitorius = dre.From<string>("korepetitorius");
				t.Mokinys = dre.From<string>("mokinys");
				t.Tvarkarastis = dre.From<DateTime?>("tvarkarastis_data")?.ToString("yyyy-MM-dd") ?? "Nepriskirta";
				t.Mokejimas = dre.From<string>("mokejimas_info") ?? "Neapmokėta";
				t.MedziaguKiekis = dre.From<int>("medziagu_kiekis");
			});

		return result;
	}

	public static PamokaCE FindPamokaCE(int id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}Pamokos` WHERE id=?id";
		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result =
			Sql.MapOne<PamokaCE>(drc, (dre, t) => {
				var pam = t.Pamoka;

				pam.Id = dre.From<int>("id");
				pam.Dalykas = dre.From<string>("Dalykas");
				pam.Ivertinimas = dre.From<int?>("Ivertinimas");
				pam.Trukme = dre.From<int?>("Trukme");
				pam.Pradzios_laikas = dre.From<int?>("Pradzios_laikas");
				pam.Nuoroda = dre.From<string>("Nuoroda");
				pam.Ikainis = dre.From<int?>("Ikainis");
				pam.FkMokinys = dre.From<int>("fk_Mokinys");
				pam.FkMokejimas = dre.From<int>("fk_Mokejimas");
				pam.FkKorepetitorius = dre.From<int>("fk_Korepetitorius");
				pam.FkTvarkarastis = dre.From<int>("fk_Tvarkarastis");
			});

		return result;
	}

	public static int InsertPamoka(PamokaCE pamCE)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}Pamokos`
			(
				`Dalykas`,
				`Ivertinimas`,
				`Trukme`,
				`Pradzios_laikas`,
				`Nuoroda`,
				`Ikainis`,
				`fk_Mokinys`,
				`fk_Mokejimas`,
				`fk_Korepetitorius`,
				`fk_Tvarkarastis`
				
			)
			VALUES(
				?dalykas,
				?ivertinimas,
				?trukme,
				?pradzios_laikas,
				?nuoroda,
				?ikainis,
				?mokinys,
				?mokejimas,
				?korepetitorius,
				?tvarkarastis
			)";

		var id =
			Sql.Insert(query, args => {
				//make a shortcut
				var pam = pamCE.Pamoka;

				//
				args.Add("?dalykas", pam.Dalykas);
				args.Add("?ivertinimas", pam.Ivertinimas);
				args.Add("?trukme", pam.Trukme);
				args.Add("?pradzios_laikas", pam.Pradzios_laikas);
				args.Add("?nuoroda", pam.Nuoroda);
				args.Add("?ikainis", pam.Ikainis);
				args.Add("?mokinys", pam.FkMokinys);
				args.Add("?mokejimas", pam.FkMokejimas);
				args.Add("?korepetitorius", pam.FkKorepetitorius);
				args.Add("?tvarkarastis", pam.FkTvarkarastis);
			});

		return (int)id;
	}

	public static void UpdatePamoka(PamokaCE pamCE)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}Pamokos`
			SET
				`Dalykas` = ?dalykas,
				`Ivertinimas` = ?ivertinimas,
				`Trukme` = ?trukme,
				`Pradzios_laikas` = ?pradzios_laikas,
				`Nuoroda` = ?nuoroda,
				`Ikainis` = ?ikainis,
				`fk_Mokinys` = ?mokinys,
				`fk_Mokejimas` = ?mokejimas,
				`fk_Korepetitorius` = ?korepetitorius,
				`fk_Tvarkarastis` = ?tvarkarastis
			WHERE id=?id";

		Sql.Update(query, args => {
			var pam = pamCE.Pamoka;
			args.Add("?dalykas", pam.Dalykas);
			args.Add("?ivertinimas", pam.Ivertinimas);
			args.Add("?trukme", pam.Trukme);
			args.Add("?pradzios_laikas", pam.Pradzios_laikas);
			args.Add("?nuoroda", pam.Nuoroda);
			args.Add("?ikainis", pam.Ikainis);
			args.Add("?mokinys", pam.FkMokinys);
			args.Add("?mokejimas", pam.FkMokejimas);
			args.Add("?korepetitorius", pam.FkKorepetitorius);
			args.Add("?tvarkarastis", pam.FkTvarkarastis);

			args.Add("?id", pam.Id);
		});
	}

	public static void DeletePamoka(int id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}Pamokos` where id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}

	

	public static List<PamokaCE.PamokosMedziagaM> ListPamokosMedziaga(int pamokaId)
	{
		
		var query =
			$@"SELECT *
			FROM `{Config.TblPrefix}Pamokos_medziagos`
			WHERE fk_Pamoka = ?pamokaId
			ORDER BY id ASC";

		var drc =
			Sql.Query(query, args => {
				args.Add("?pamokaId", pamokaId);
			});

		var result =
			Sql.MapAll<PamokaCE.PamokosMedziagaM>(drc, (dre, t) => {
				t.Paskirtis = dre.From<string>("Paskirtis");
				t.NaudojimosiTrukme = dre.From<int?>("Naudojimosi_trukme");
				t.Pastabos = dre.From<string>("Pastabos");
				t.FkMokomojiMedziaga = dre.From<int>("fk_Mokomoji_medziaga");
			});

		
		for (int i = 0; i < result.Count; i++)
			result[i].InListId = i;

		return result;
	}

	public static void InsertPamokosMedziaga(int pamokaId, PamokaCE.PamokosMedziagaM pm)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}Pamokos_medziagos`
				(
					fk_Pamoka,
					fk_Mokomoji_medziaga,
					Paskirtis,
					Naudojimosi_trukme,
					Pastabos
				)
				VALUES(
					?fk_Pamoka,
					?fk_Mokomoji_medziaga,
					?paskirtis,
					?trukme,
					?pastabos
				)";

		Sql.Insert(query, args => {
			args.Add("?fk_Pamoka", pamokaId);
			args.Add("?fk_Mokomoji_medziaga", pm.FkMokomojiMedziaga);
			args.Add("?paskirtis", pm.Paskirtis);
			args.Add("?trukme", pm.NaudojimosiTrukme);
			args.Add("?pastabos", pm.Pastabos);
		});
	}

	public static void DeletePamokosMedziagasForPamoka(int pamokaId)
	{
		
		var query =
			$@"DELETE FROM `{Config.TblPrefix}Pamokos_medziagos`
			WHERE fk_Pamoka=?pamokaId";

		Sql.Delete(query, args => {
			args.Add("?pamokaId", pamokaId);
		});
	}
}
/*/// <summary>
/// Database operations related to 'Sutartis' entity.
/// </summary>
public class SutartisF2Repo : RepoBase
{
	public static List<PamokaL> ListSutartis()
	{
		var query =
			$@"SELECT
				s.nr,
				s.sutarties_data as data,
				CONCAT(d.vardas,' ', d.pavarde) as darbuotojas,
				CONCAT(n.vardas,' ',n.pavarde) as nuomininkas,
				b.name as busena
			FROM
				`{Config.TblPrefix}sutartys` s
				LEFT JOIN `{Config.TblPrefix}darbuotojai` d ON s.fk_darbuotojas=d.tabelio_nr
				LEFT JOIN `{Config.TblPrefix}klientai` n ON s.fk_klientas=n.asmens_kodas
				LEFT JOIN `{Config.TblPrefix}sutarties_busenos` b ON s.busena=b.id
			ORDER BY s.nr DESC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<PamokaL>(drc, (dre, t) => {
				t.Nr = dre.From<int>("nr");
				t.Darbuotojas = dre.From<string>("darbuotojas");
				t.Nuomininkas = dre.From<string>("nuomininkas");
				t.Data = dre.From<DateTime>("data");
				t.Busena = dre.From<string>("busena");
			});

		return result;
	}

	public static PamokaCE FindSutartisCE(int nr)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}sutartys` WHERE nr=?nr";
		var drc =
			Sql.Query(query, args => {
				args.Add("?nr", nr);
			});

		var result =
			Sql.MapOne<PamokaCE>(drc, (dre, t) => {
				//make a shortcut
				var sut = t.Sutartis;

				//
				sut.Nr = dre.From<int>("nr");
				sut.SutartiesData = dre.From<DateTime>("sutarties_data");
				sut.NuomosDataLaikas = dre.From<DateTime>("nuomos_data_laikas");
				sut.PlanuojamaGrDataLaikas = dre.From<DateTime>("planuojama_grazinimo_data_laikas");
				sut.FaktineGrDataLaikas = dre.From<DateTime?>("faktine_grazinimo_data_laikas");
				sut.PradineRida = dre.From<int>("pradine_rida");
				sut.GalineRida = dre.From<int?>("galine_rida");
				sut.Kaina = dre.From<decimal>("kaina");
				sut.DegaluKiekisPaimant = dre.From<int>("degalu_kiekis_paimant");
				sut.DegaluKiekisGrazinant = dre.From<int?>("dagalu_kiekis_grazinus");
				sut.FkBusena = dre.From<int>("busena");
				sut.FkKlientas = dre.From<string>("fk_klientas");
				sut.FkDarbuotojas = dre.From<string>("fk_darbuotojas");
				sut.FkAutomobilis = dre.From<int>("fk_automobilis");
				sut.FkGrazinimoVieta = dre.From<int>("fk_grazinimo_vieta");
				sut.FkPaemimoVieta = dre.From<int>("fk_paemimo_vieta");
			});

		return result;
	}

	public static int InsertSutartis(PamokaCE sutCE)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}sutartys`
			(
				`sutarties_data`,
				`nuomos_data_laikas`,
				`planuojama_grazinimo_data_laikas`,
				`faktine_grazinimo_data_laikas`,
				`pradine_rida`,
				`galine_rida`,
				`kaina`,
				`degalu_kiekis_paimant`,
				`dagalu_kiekis_grazinus`,
				`busena`,
				`fk_klientas`,
				`fk_darbuotojas`,
				`fk_automobilis`,
				`fk_grazinimo_vieta`,
				`fk_paemimo_vieta`
			)
			VALUES(
				?sutdata,
				?nuomdata,
				?plgrlaikas,
				?fkgrlaikas,
				?prrida,
				?glrida,
				?kaina,
				?dkiekispa,
				?dkiekisgr,
				?busena,
				?klientas,
				?darbuotojas,
				?automobilis,
				?grvieta,
				?pavieta
			)";

		var nr =
			Sql.Insert(query, args => {
				//make a shortcut
				var sut = sutCE.Sutartis;

				//
				args.Add("?sutdata", sut.SutartiesData);
				args.Add("?nuomdata", sut.NuomosDataLaikas);
				args.Add("?plgrlaikas", sut.PlanuojamaGrDataLaikas);
				args.Add("?fkgrlaikas", sut.FaktineGrDataLaikas);
				args.Add("?prrida", sut.PradineRida);
				args.Add("?glrida", sut.GalineRida);
				args.Add("?kaina", sut.Kaina);
				args.Add("?dkiekispa", sut.DegaluKiekisPaimant);
				args.Add("?dkiekisgr", sut.DegaluKiekisGrazinant);
				args.Add("?busena", sut.FkBusena);
				args.Add("?darbuotojas", sut.FkDarbuotojas);
				args.Add("?klientas", sut.FkKlientas);
				args.Add("?automobilis", sut.FkAutomobilis);
				args.Add("?grvieta", sut.FkGrazinimoVieta);
				args.Add("?pavieta", sut.FkPaemimoVieta);
			});

		return (int)nr;
	}

	public static void UpdateSutartis(PamokaCE sutCE)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}sutartys`
			SET
				`sutarties_data` = ?sutdata,
				`nuomos_data_laikas` = ?nuomdata,
				`planuojama_grazinimo_data_laikas` = ?plgrlaikas,
				`faktine_grazinimo_data_laikas` = ?fkgrlaikas,
				`pradine_rida` = ?prrida,
				`galine_rida` = ?glrida,
				`kaina` = ?kaina,
				`degalu_kiekis_paimant` = ?dkiekispa,
				`dagalu_kiekis_grazinus` = ?dkiekisgr,
				`busena` = ?busena,
				`fk_klientas` = ?klientas,
				`fk_darbuotojas` = ?darbuotojas,
				`fk_automobilis` = ?automobilis,
				`fk_grazinimo_vieta` = ?grvieta,
				`fk_paemimo_vieta` = ?pavieta
			WHERE nr=?nr";

		Sql.Update(query, args => {
			//make a shortcut
			var sut = sutCE.Sutartis;

			//
			args.Add("?sutdata", sut.SutartiesData);
			args.Add("?nuomdata", sut.NuomosDataLaikas);
			args.Add("?plgrlaikas", sut.PlanuojamaGrDataLaikas);
			args.Add("?fkgrlaikas", sut.FaktineGrDataLaikas);
			args.Add("?prrida", sut.PradineRida);
			args.Add("?glrida", sut.GalineRida);
			args.Add("?kaina", sut.Kaina);
			args.Add("?dkiekispa", sut.DegaluKiekisPaimant);
			args.Add("?dkiekisgr", sut.DegaluKiekisGrazinant);
			args.Add("?busena", sut.FkBusena);
			args.Add("?darbuotojas", sut.FkDarbuotojas);
			args.Add("?klientas", sut.FkKlientas);
			args.Add("?automobilis", sut.FkAutomobilis);
			args.Add("?grvieta", sut.FkGrazinimoVieta);
			args.Add("?pavieta", sut.FkPaemimoVieta);

			args.Add("?nr", sut.Nr);
		});
	}

	public static void DeleteSutartis(int nr)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}sutartys` where nr=?nr";
		Sql.Delete(query, args => {
			args.Add("?nr", nr);
		});
	}

	public static List<SutartiesBusena> ListSutartiesBusena()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}sutarties_busenos` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<SutartiesBusena>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Name = dre.From<string>("name");
			});

		return result;
	}

	public static List<PamokaCE.PamokosMedziagaM> ListUzsakytaPaslauga(int sutartisId)
	{
		var query =
			$@"SELECT *
			FROM `{Config.TblPrefix}uzsakytos_paslaugos`
			WHERE fk_sutartis = ?sutartisId
			ORDER BY fk_paslauga ASC, fk_kaina_galioja_nuo ASC";

		var drc =
			Sql.Query(query, args => {
				args.Add("?sutartisId", sutartisId);
			});

		var result =
			Sql.MapAll<PamokaCE.PamokosMedziagaM>(drc, (dre, t) => {
				t.Paslauga =
					//we use JSON here to make serialization/deserializaton of composite key more convenient
					JsonConvert.SerializeObject(new {
						FkPaslauga = dre.From<int>("fk_paslauga"),
						GaliojaNuo = dre.From<DateTime>("fk_kaina_galioja_nuo")
					});
				t.Kiekis = dre.From<int>("kiekis");
				t.Kaina = dre.From<decimal>("kaina");
			});

		for( int i = 0; i < result.Count; i++ )
			result[i].InListId = i;

		return result;
	}

	public static void InsertUzsakytaPaslauga(int sutartisId, PamokaCE.PamokosMedziagaM up)
	{
		//deserialize 'Paslauga' foreign keys from 'UzsakytaPaslauga' view model key
		var fks =
			JsonConvert.DeserializeAnonymousType(
				up.Paslauga,
				//this creates object of correct shape that is filled in by the JSON deserializer
				new {
					FkPaslauga = 1,
					GaliojaNuo = DateTime.Now
				}
			);

		//
		var query =
			$@"INSERT INTO `{Config.TblPrefix}uzsakytos_paslaugos`
				(
					fk_sutartis,
					fk_kaina_galioja_nuo,
					fk_paslauga,
					kiekis,
					kaina
				)
				VALUES(
					?fk_sutartis,
					?galioja_nuo,
					?fk_paslauga,
					?kiekis,
					?kaina
				)";

		Sql.Insert(query, args => {
			args.Add("?fk_sutartis", sutartisId);
			args.Add("?galioja_nuo", fks.GaliojaNuo);
			args.Add("?fk_paslauga", fks.FkPaslauga);
			args.Add("?kaina", up.Kaina);
			args.Add("?kiekis", up.Kiekis);
		});
	}

	public static void DeleteUzsakytaPaslaugaForSutartis(int sutartis)
	{
		var query =
			$@"DELETE FROM a
			USING `{Config.TblPrefix}uzsakytos_paslaugos` as a
			WHERE a.fk_sutartis=?fkid";

		Sql.Delete(query, args => {
			args.Add("?fkid", sutartis);
		});
	}
}*/