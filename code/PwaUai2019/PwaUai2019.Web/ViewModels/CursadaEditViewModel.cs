using System.Collections.Generic;
using System.ComponentModel;
using PwaUai2019.Web.Helpers;
using PwaUai2019.Web.Models;

namespace PwaUai2019.Web.ViewModels
{
    public class CursadaEditViewModel
    {
        public CursadaEditViewModel()
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
        }

        public Cursada Cursada { get; set; }

        public List<Turno> Turnos { get; set; }

        public List<Day> Dias { get; set; }
    }
}
