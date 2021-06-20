using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVTOS
{
    class QueryList
    {
        public static string MostraMieSquadre(string codiceFiscale)
        {
            return "SELECT squadra.Nome, squadra.IdSquadra " +
                "FROM squadra join adesione_player_squadra on adesione_player_squadra.IdSquadra = squadra.IdSquadra " +
                "WHERE CF_Player = '" + codiceFiscale + "'and DataFine is null; ";
        }

        public static string LasciaSquadra(string codiceFiscale, int idSquadra)
        {
            return "UPDATE adesione_player_squadra " +
                "SET DataFine = now() " +
                "WHERE CF_Player = '" + codiceFiscale + "'  AND IdSquadra = " + idSquadra + "  AND DataFine is null; ";
        }

        public static string EntraSquadra(string codiceFiscale, int idSquadra)
        {
            return "INSERT into adesione_player_squadra values( " + idSquadra + ", '" + codiceFiscale + "', now(), null);";
        }


        public static string MostraSqNonComplete(string codiceFiscale)
        {
            return "SELECT Nome, count(*) as membri " +
                "FROM adesione_player_squadra join squadra on squadra.IdSquadra = adesione_player_squadra.IdSquadra " +
                "WHERE DataFine is null AND squadra.IdSquadra not in (SELECT squadra.IdSquadra " +
                                                                        "FROM squadra join adesione_player_squadra on adesione_player_squadra.IdSquadra = squadra.IdSquadra " +
                                                                        "WHERE CF_Player = '" + codiceFiscale + "'and DataFine is null) " +
                "GROUP BY squadra.IdSquadra " +
                "HAVING membri< 5; "
;
        }

        internal static string InsertBiglietto(string id1,string id2,string data,string idarena,string costo)
        {
            return "INSERT INTO biglietto VALUES();";
        }

        public static string VisualizzaPlayer()
        {
            return "SELECT * FROM ivtos.player;";
        }
        public static string VisualizzaVideogiochi()
        {
            return "SELECT videogioco.nome AS Nome, videogioco.DataCreazione, azienda_videogioco.Nome AS Azienda, tipologia_gioco.Tipo AS Tipologia " +
                "from(videogioco JOIN azienda_videogioco ON videogioco.partitaivaazienda = azienda_videogioco.partitaiva) join tipologia_gioco on videogioco.tipologiagioco = tipologia_gioco.idtipologia; ";
        }

        internal static string VisualizzaArbitri()
        {
            return "SELECT * FROM ivtos.Arbitro;";
        }

        internal static string VisualizzaSpeaker()
        {
            return "SELECT * FROM ivtos.speaker;";
        }

        internal static string VisualizzaCoach()
        {
            return "SELECT * FROM ivtos.coach;";
        }

        internal static string VisualizzaTorneiAttivi()
        {
            return "SELECT idTorneo AS Torneo, nomevideogioco AS Videogioco, nomearena AS Arena, sponsor.Nome, DataInizio, nmaxiscrizioni AS NumeroSquadre " +
                        "FROM(torneo JOIN Arena ON torneo.IdArena = arena.IdArena)JOIN Sponsor on torneo.IdSponsor = sponsor.idsponsor " +
                        "WHERE torneo.datafine is null; ";
        }

        internal static string VisualizzaVideogiochiTornei()
        {
            return "SELECT videogioco.Nome, count(*) AS NumTorneiGiocati" +
                "from videogioco join torneo on videogioco.nome = torneo.NomeVideogioco " +
                "group by videogioco.Nome Order by count(*) desc " +
                "Limit 3; ";
        }

        internal static string VisualizzaSquadraTornei()
        {
            return "select squadra.idSquadra, squadra.Nome, count(*) NumTorneiGiocati " +
                "from squadra join iscrizione on squadra.idsquadra = iscrizione.idsquadra " +
                "group by iscrizione.idsquadra " +
                "order by count(*) desc " +
                "limit 1; ";
        }

        internal static string VisualizzaPlayerTornei()
        {
            return "select player.nickname,player.genere, squadra.Nome AS NomeSquadra, count(*) NumTorneiGiocati " +
                "from squadra " +
                "join iscrizione on squadra.idsquadra = iscrizione.idsquadra " +
                "join adesione_player_squadra on adesione_player_squadra.idSquadra = squadra.idsquadra " +
                "join player on adesione_player_squadra.cf_player = player.cf " +
                "group by adesione_player_squadra.CF_player " +
                "order by count(*) desc " +
                "limit 1; ";
        }

        internal static string VisualizzaTorneiBiglietti()
        {
            return "select partita.idtorneo, count(*) AS NumBigliettiVenduti, torneo.DataInizio, torneo.NomeVideogioco " +
                "from partita " +
                "join torneo " +
                "join biglietto on partita.idsquadra1 = biglietto.idsquadra1 AND partita.idsquadra2 = biglietto.idsquadra2 AND partita.dataora = biglietto.dataora " +
                "join acquisto_biglietto on acquisto_biglietto.idsquadra1 = biglietto.idsquadra1 AND acquisto_biglietto.idsquadra2 = biglietto.idsquadra2 " +
                "AND acquisto_biglietto.dataora = biglietto.dataora AND biglietto.idarena = acquisto_biglietto.idarena " +
                "group by partita.idtorneo " +
                "order by count(*) desc " +
                "limit 20; ";
        }

        internal static string VisualizzaTorneiConMenoSquadre()
        {
            return "Select torneo.idTorneo, count(*) AS NumeroSquadre, torneo.DataInizio,torneo.NomeVideogioco,sponsor.Nome " +
                "from(torneo join iscrizione on torneo.idtorneo = iscrizione.idtorneo) join sponsor on torneo.idsponsor = sponsor.idsponsor " +
                "group by torneo.idtorneo " +
                "order by count(*) asc " +
                "limit 20; ";
        }

        internal static string VisualizzaTorneiConPiuSquadre()
        {
            return "Select torneo.idTorneo, count(*) AS NumeroSquadre, torneo.DataInizio,torneo.NomeVideogioco,sponsor.Nome " +
                "from torneo join iscrizione on torneo.idtorneo = iscrizione.idtorneo " +
                "join sponsor on torneo.idsponsor = sponsor.idsponsor " +
                "group by torneo.idtorneo " +
                "order by count(*) desc " +
                "limit 20; ";
        }

        internal static string CreaSquadra(string nome)
        {
            return "INSERT INTO squadra VALUES('"+nome+"',IdSquadra,now())";
        }

        public static string VisualizzaAziendeGiochi()
        {
            return "SELECT * FROM ivtos.azienda_videogioco;";
        }
        public static string VisualizzaSquadre()
        {
            return "SELECT * FROM ivtos.squadra;";
        }
        public static string VisualizzaChiavePartite()
        {
            return "SELECT idsquadra1, idsquadra2, dataora  FROM ivtos.squadra;";
        }
        public static string VisualizzaStati()
        {
            return "SELECT * FROM ivtos.stato;";
        }
        public static string VisualizzaCitta()
        {
            return "SELECT * FROM ivtos.cittá;";
        }
        public static string VisualizzaArena()
        {
            return "SELECT * FROM ivtos.arena;";
        }
        public static string VisualizzaTornei()
        {
            return "SELECT idTorneo AS Torneo, nomevideogioco AS Videogioco, nomearena AS Arena, sponsor.Nome, DataInizio, DataFine, nmaxiscrizioni AS NumeroSquadre " +
                        "FROM(torneo JOIN Arena ON torneo.IdArena = arena.IdArena) JOIN Sponsor on torneo.IdSponsor = sponsor.idsponsor ; ";
        }
        public static string VisualizzaSponsor()
        {
            return "SELECT * FROM ivtos.sponsor;";
        }
        public static string VisualizzaIscrizioniTorneo(string torneo)
        {
            return "SELECT squadra.idsquadra as IdSquadra, squadra.nome AS NomeSquadra" +
                " from iscrizione join squadra on iscrizione.IdSquadra = squadra.IdSquadra " +
                "where idTorneo = " + torneo + "; ";
        }
        public static string VisualizzaIscrizioniSquadra(string torneo)
        {
            return "SELECT torneo.idtorneo as IdTorneo, torneo.datainizio AS DataDiInizio, torneo.NomeVideogioco, sponsor.Nome AS Sponsor " +
                "from(iscrizione join torneo on iscrizione.Idtorneo = torneo.idtorneo) join sponsor on torneo.IdSponsor = sponsor.idsponsor " +
                "where IdSquadra = " + torneo + "";
        }
        public static string VisualizzaPartiteTorneo(string torneo)
        {
            return "SELECT IdSquadra1 as \"SQUADRA OSPITE\", IdSquadra2 as \"SQUADRA CASA\"," +
                " DataOra as \"DATA E ORA\" FROM ivtos.partita WHERE partita.IdTorneo = "+ torneo + "; ";
        }
        public static string TerminaTorneo(string torneo)
        {
            return "UPDATE torneo " +
                "SET DataFine = now() " +
                "WHERE idtorneo = " + torneo + "; ";
        }
        public static string IscriviSqATorneo(int idSquadra, int idTorneo)
        {
            return "INSERT INTO iscrizione VALUES (" + idTorneo + "," + idSquadra + "); ";
        }

        public static string NomeArenaInCuiSiSvolgePartita(string idSquadra1, string idSquadra2, string data)
        {
            return "SELECT A.NomeArena FROM(partita P join torneo T on P.IdTorneo = T.IdTorneo) join Arena A on T.IdArena = A.IdArena " +
                "WHERE P.IdSquadra1 = " + idSquadra1 +
                " AND P.IdSquadra2 = " + idSquadra2 +
                " AND P.DataOra = '" + data + "';";
        }
        public static string CittaArenaInCuiSiSvolgePartita(string idSquadra1, string idSquadra2, string data)
        {
            return "SELECT A.NomeCitta FROM(partita P join torneo T on P.IdTorneo = T.IdTorneo) join Arena A on T.IdArena = A.IdArena " +
                "WHERE P.IdSquadra1 = " + idSquadra1 +
                " AND P.IdSquadra2 = " + idSquadra2 +
                " AND P.DataOra = '" + data + "';";
        }
        public static string StatoArenaInCuiSiSvolgePartita(string idSquadra1, string idSquadra2, string data)
        {
            return "SELECT A.NomeStato FROM(partita P join torneo T on P.IdTorneo = T.IdTorneo) join Arena A on T.IdArena = A.IdArena " +
                "WHERE P.IdSquadra1 = " + idSquadra1 +
                " AND P.IdSquadra2 = " + idSquadra2 +
                " AND P.DataOra = '" + data + "';";
        }
        public static string IdArenaInCuiSiSvolgePartita(string idSquadra1, string idSquadra2, string data)
        {
            return "SELECT A.IdArena FROM(partita P join torneo T on P.IdTorneo = T.IdTorneo) join Arena A on T.IdArena = A.IdArena " +
                "WHERE P.IdSquadra1 = " + idSquadra1 +
                " AND P.IdSquadra2 = " + idSquadra2 +
                " AND P.DataOra = '" + data + "';";
        }

        public static string PrezzoBiglietto(string idArena)
        {
            return "SELECT B.Costo FROM Arena A join biglietto B on A.IdArena = B.IdArena WHERE a.IdArena =  " + idArena;
        }
        public static string CompraBiglietto(string idSquadra1, string idSquadra2, string data, string idArena, string cf)
        {
            return "INSERT INTO acquisto_biglietto VALUES (" + idArena + "," + idSquadra1 + "," + idSquadra2 + ",'" + data + "','" + cf + "');";
        }

        public static string MostraMembriSquadra(int idSquadra)
        {
            return "SELECT  player.cognome, player.nome, player.Nickname " +
                "from(adesione_player_squadra join squadra on adesione_player_squadra.IdSquadra = squadra.IdSquadra) join player on adesione_player_squadra.CF_Player = player.CF " +
                "where adesione_player_squadra.DataFine is null and squadra.IdSquadra = " + idSquadra;

        }

        public static string MostraProssimePartita(int idSquadra)
        {
            return "select s1.nome, s2.nome,dataora, nomearena, nomecitta " +
                "from partita join squadra s1 on partita.idsquadra1 = s1.idSquadra join squadra s2 on partita.idsquadra2 = s2.idSquadra join torneo on partita.idtorneo = torneo.idtorneo join arena on torneo.idarena = arena.idarena " +
                "where idsquadra1 = "+idSquadra+ " or idsquadra2 = " + idSquadra +" " +
                "order by dataora";
        }
    }
}
