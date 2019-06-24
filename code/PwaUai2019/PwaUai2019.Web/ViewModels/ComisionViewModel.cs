using System.Collections.Generic;
using System.ComponentModel;
using PwaUai2019.Web.Helpers;
using PwaUai2019.Web.Models;

namespace PwaUai2019.Web.ViewModels
{
    public class ComisionViewModel
    {
        public ComisionViewModel()
        {
            Carreras = CarreraHelper.GetCarreras();
            Cursadas = new List<Cursada>();
            Comisiones = CarreraHelper.GetComision();
        }

        [DisplayName("Carrera")]
        public List<string> Carreras { get; set; }

        public string Carrera { get; set; }

        [DisplayName("Comisiones")]
        public List<string> Comisiones { get; set; }

        public string Comision { get; set; }

        public IEnumerable<Cursada> Cursadas { get; set; }
    }
}
