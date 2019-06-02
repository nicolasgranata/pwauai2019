using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PwaUai2019.Web.Helpers
{
    public static class CarreraHelper
    {
        public static List<string> GetCarreras()
        {
            var carreras = new List<string>()
            {
                "Ingenieria en Sistemas",
                "Licenciatura en Gest. Tec. Informática",
                "Licenciatura en Comunicacion Social",
                "Licenciatura en Relaciones del Trabajo",
                "Licenciatura en Psicología",
                "Licenciatura en Matematica",
                "Medicina",
                "Ingenieria Civil",
                "Arquitectura",
                "Diseño gráfico",
            };

            return carreras;
        }
    }
}
