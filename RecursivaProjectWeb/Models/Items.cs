using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursivaProjectWeb.Models
{
    public class Items
    {
        public int CantTotalPersonas { get; set; }

        public int PromedioEdadClubRacing { get; set; }

        public List<Item1> item1 { get; set; }

        public List<Item2> item2 { get; set; }

        public List<QueryObject> item3 { get; set; }


    }
}