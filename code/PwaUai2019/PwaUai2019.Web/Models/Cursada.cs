using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PwaUai2019.Web.Models
{
    public class Cursada
    {
        public long Id { get; set; }
        public string Materia { get; set; }
        public string Carrera { get; set; }
        public int CantidadAlumnos { get; set; }
        public string Turno { get; set; }
        public DateTime Horario { get; set; }
        public string Comision { get; set; }
    }
}
