using System;
using System.ComponentModel;

namespace PwaUai2019.Web.Models
{
    public class Cursada
    {
        public long Id { get; set; }

        public string Materia { get; set; }

        public string Carrera { get; set; }

        [DisplayName("Cantidad de Alumnos")]
        public int CantidadAlumnos { get; set; }

        public string Turno { get; set; }

        public DateTime Horario { get; set; }

        public string Comision { get; set; }

        public Aula Aula { get; set; }
    }
}
