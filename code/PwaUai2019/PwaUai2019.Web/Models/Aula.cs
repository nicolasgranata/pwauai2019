using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PwaUai2019.Web.Models
{
    public class Aula
    {
        public Aula()
        {
            Cursadas = new List<Cursada>();
        }
        
        public long Id { get; set; }

        public int Numero { get; set; }

        public int Piso { get; set; }

        public int Capacidad { get; set; }

        public string Nombre => $"P{Piso.ToString()}N{Numero.ToString()}";

        public IEnumerable<Cursada> Cursadas { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

        [JsonIgnore]
        public string Error { get; set; }
    }
}
