using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursivaProjectWeb.Models
{
    public class Persona
    {

        public string Nombre { get; set; }

        public int Edad { get; set; }

        public string Club { get; set; }

        public string EstadoCivil { get; set; }

        public string NivelEstudio { get; set; }

        public Persona() { }
        public Persona(string nombre, int edad, string club, string estadocivil, string nivelEstudio)
        {
            Nombre = nombre;
            Edad = edad;
            Club = club;
            EstadoCivil = estadocivil;
            NivelEstudio = nivelEstudio;
        }

    }
}