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

    }
}
