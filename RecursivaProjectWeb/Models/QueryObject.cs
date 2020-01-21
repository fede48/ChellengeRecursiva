using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursivaProjectWeb.Models
{
    public class QueryObject
    {
        public string Club { get; set; }
        public int CantSocios { get; set; }
        public int PromedioEdadSocios { get; set; }
        public int MenorEdadClub { get; set; }
        public int MayorEdadClub { get; set; }

        public QueryObject() { }
        public QueryObject(string club, int cantSocios, int promedioEdadSocios, int menorEdadClub, int mayorEdadClub)
        {
            Club = club;
            CantSocios = cantSocios;
            PromedioEdadSocios = promedioEdadSocios;
            MenorEdadClub = menorEdadClub;
            MayorEdadClub = mayorEdadClub;
        }
    }
}