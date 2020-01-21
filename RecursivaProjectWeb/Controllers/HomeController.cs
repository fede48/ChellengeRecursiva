using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecursivaProjectWeb.Models;


namespace RecursivaProjectWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new UploadFileModel());
        }

        public ActionResult Data(Items item)
        {
            return View(new Items());
        }


        [HttpPost]
        public ActionResult FileUpload(UploadFileModel model)
        {
            var lines = new List<string>();
            int cantTotalPersonas = 0;
            var myModel = new Items();
            if ( model.Files[0] != null && model.Files.Count > 0)
            {  
                using (StreamReader reader = new StreamReader(model.Files[0].InputStream))
                {
                    do
                    {
                        string textLine = reader.ReadLine();
                        lines.Add(textLine);
                    } while (reader.Peek() != -1);

                    
                    var personas = new List<Persona>();
                    foreach (var item in lines)
                    {
                        var person = new Persona();
                        var listAttr = item.Split(';');
                        person.Nombre = item.Split(';')[0];
                        person.Edad = Convert.ToInt32(item.Split(';')[1]);
                        person.Club = item.Split(';')[2];
                        person.EstadoCivil = item.Split(';')[3];
                        person.NivelEstudio = item.Split(';')[4];
                        personas.Add(person);
                    }

                    //1.Cantidad total de personas registradas.
                    cantTotalPersonas = personas.Count;
                    int cantTotalSociosRacing = personas.Where(e => e.Club == "Racing").Count();
                    int sumaPromedioRacing = personas.Where(e => e.Club == "Racing").Select(x => x.Edad).Sum();
                    
                    //2.El promedio de edad de los socios de Racing.
                    int promedioEdadClubRacing = sumaPromedioRacing / cantTotalSociosRacing;

                    /*
                     3. Un listado con las 100 primeras personas casadas, con estudios Universitarios, 
                     ordenadas de menor a mayor según su edad. 
                     Por cada persona, mostrar: nombre, edad y equipo.

                     */
                    var query = (from pro in personas
                                 where pro.EstadoCivil == "Casado" && pro.NivelEstudio == "Universitario"
                                 orderby pro.Edad ascending
                                 select new { pro.Nombre, pro.Edad, pro.Club }).Take(100).ToList(); 


                    var listRiver = personas.Where(e => e.Club == "River").ToList();

                    //4. Un listado con los 5 nombres más comunes entre los hinchas de River.
                    var query2 = listRiver
                                .GroupBy(x => x.Nombre)
                                .Where(g => g.Count() > 1)
                                .Select(y => new { y.Key, Counter = y.Count() }).OrderByDescending(x => x.Counter).Take(5).ToList();



                    /* 5. Un listado, ordenado de mayor a menor según la cantidad de socios, que enumere, junto con cada equipo,
                     el promedio de edad de sus socios, la menor edad registrada y la mayor edad registrada. */

                    var query3 = personas
                                .GroupBy(x => x.Club)
                                .Select(g => new { g.Key, List = g.ToList() })
                                .ToList();


                    var listQueryObject = new List<QueryObject>();
                    var QueryObjectInDescOrder = new List<QueryObject>();
                    if (query3 != null && query3.Count > 0)
                    {
                           foreach (var item in query3)
                           {

                               var promedioEdad = item.List.Select(x => x.Edad).Sum() / item.List.Count;
                               var menorEdad = item.List.Select(x => x.Edad).Min();
                               var mayorEdad = item.List.Select(x => x.Edad).Max();
                               var queryObject = new QueryObject(item.Key, item.List.Count, promedioEdad, menorEdad, mayorEdad);
                               listQueryObject.Add(queryObject);
                            }
                            QueryObjectInDescOrder = listQueryObject.OrderByDescending(x => x.CantSocios).ToList();

                    }

                    var item1 = (from elem in query
                                 select new Item1
                                 {
                                     Nombre = elem.Nombre,
                                     Edad = elem.Edad,
                                     Club = elem.Club
                                 }).ToList();

                    var item2 = (from elem in query2
                                 select new Item2
                                 {
                                     Club = elem.Key,
                                     Cantidad = elem.Counter
                                 }).ToList();


                    myModel = new Items
                    {
                        CantTotalPersonas = cantTotalPersonas,
                        PromedioEdadClubRacing = promedioEdadClubRacing,
                        item1 = item1,
                        item2 = item2,
                        item3 = QueryObjectInDescOrder
                    };

                    return View("Data", myModel);
                }

            }
            return View(myModel);
        }
  
        

    }
}