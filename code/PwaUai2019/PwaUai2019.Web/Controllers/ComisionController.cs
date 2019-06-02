using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PwaUai2019.Web.Services;
using PwaUai2019.Web.ViewModels;

namespace PwaUai2019.Web.Controllers
{
    public class ComisionController : Controller
    {
        private readonly ICursadaService _cursadaService;

        public ComisionController(ICursadaService cursadaService)
        {
            _cursadaService = cursadaService;
        }

        public IActionResult Index()
        {
            return View(new ComisionViewModel());
        }

        [HttpGet]
        public IActionResult Cursada(string carrera, string comision)
        {
            var cursadas = _cursadaService.GetAll().Where(x => x.Carrera.ToUpper() == carrera.ToUpper() & x.Comision.ToUpper() == comision.ToUpper()); 
            return PartialView("_IndexPartial", cursadas);
        }
    }
}