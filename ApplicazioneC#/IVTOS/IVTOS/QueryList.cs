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

        public static string VisualizzaPlayer()
        {
            return "SELECT * FROM ivtos.player;";
        }
        public static string VisualizzaVideogiochi()
        {
            return "SELECT videogioco.nome AS Nome, videogioco.DataCreazione, azienda_videogioco.Nome AS Azienda, tipologia_gioco.Tipo AS Tipologia " +
                "from(videogioco JOIN azienda_videogioco ON videogioco.partitaivaazienda = azienda_videogioco.partitaiva) join tipologia_gioco on videogioco.tipologiagioco = tipologia_gioco.idtipologia; ";
        }

        internal static string VisualizzaTorneiAttivi()
        {
            return "SELECT idTorneo AS Torneo, nomevideogioco AS Videogioco, nomearena AS Arena, sponsor.Nome, DataInizio, nmaxiscrizioni AS Capienza " +
                        "FROM(torneo JOIN Arena ON torneo.IdArena = arena.IdArena)JOIN Sponsor on torneo.IdSponsor = sponsor.idsponsor " +
                        "WHERE torneo.datafine is null; ";
        }

        internal static string VisualizzaVideogiochiTornei()
        {
            return "SELECT videogioco.Nome, count(*)" +
                "from videogioco join torneo on videogioco.nome = torneo.NomeVideogioco " +
                "group by videogioco.Nome Order by count(*) desc " +
                "Limit 3; ";
        }

        internal static string VisualizzaSquadraTornei()
        {
            return null;
        }

        internal static string VisualizzaPlayerTornei()
        {
            return null;
        }

        internal static string VisualizzaTorneiBiglietti()
        {
            return null;
        }

        internal static string VisualizzaTorneiConMenoSquadre()
        {
            return null;
        }

        internal static string VisualizzaTorneiConPiuSquadre()
        {
            return null;
        }

        public static string VisualizzaAziendeGiochi()
        {
            return "SELECT * FROM ivtos.azienda_videogioco;";
        }
        public static string VisualizzaSquadre()
        {
            return "SELECT * FROM ivtos.squadra;";
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
            return "SELECT idTorneo AS Torneo, nomevideogioco AS Videogioco, nomearena AS Arena, sponsor.Nome, DataInizio, DataFine, nmaxiscrizioni AS Capienza " +
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
                "where idTorneo = "+ torneo +"; ";
        }
        public static string VisualizzaIscrizioniSquadra(string torneo)
        {
            return "SELECT torneo.idtorneo as IdTorneo, torneo.datainizio AS DataDiInizio, torneo.NomeVideogioco, sponsor.Nome AS Sponsor " +
                "from(iscrizione join torneo on iscrizione.Idtorneo = torneo.idtorneo) join sponsor on torneo.IdSponsor = sponsor.idsponsor " +
                "where IdSquadra = "+torneo+""; 
        }
        public static string TerminaTorneo(string torneo)
        {
            return "UPDATE torneo " +
                "SET DataFine = now() " +
                "WHERE idtorneo = " + torneo + "; ";
        }
        public static string EliminaTorneo(string torneo)
        {
            return "DELETE FROM torneo WHERE IdTorneo=" + torneo +";";
        }
    }
}
