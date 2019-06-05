using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PwaUai2019.Web.Helpers;
using PwaUai2019.Web.Models;

namespace PwaUai2019.Web.ViewModels
{
    public class CursadaCreateViewModel
    {
        public CursadaCreateViewModel()
        {
            Turnos = new List<Turno>()
            {
                Turno.Mañana,
                Turno.Tarde,
                Turno.Noche
            };

            Dias = new List<Day>()
            {
                Day.Lunes,
                Day.Martes,
                Day.Miercoles,
                Day.Jueves,
                Day.Viernes,
                Day.Sabado
            };

            Carreras = CarreraHelper.GetCarreras();
        }

        public Cursada Cursada { get; set; }


        public List<Turno> Turnos { get; set; }

        public List<Day> Dias { get; set; }

        [DisplayName("Carrera")]
        public List<string> Carreras { get; set; }
    }
}
