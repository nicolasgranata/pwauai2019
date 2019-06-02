using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PwaUai2019.Web.Models;
using PwaUai2019.Web.Services;
using PwaUai2019.Web.ViewModels;

namespace PwaUai2019.Web.Controllers
{
    public class CursadaController : Controller
    {
        private readonly IAulaService _aulaService;
        private readonly ICursadaService _cursadaService;

        public CursadaController(ICursadaService cursadaService, IAulaService aulaService)
        {
            _cursadaService = cursadaService;
            _aulaService = aulaService;
        }

        public IActionResult Index()
        {
            IEnumerable<Cursada> cursadas = _cursadaService.GetAll();
            return View(cursadas);
        }

        public IActionResult Create()
        {
            var cursadaViewModel = new CursadaCreateViewModel();
            cursadaViewModel.AulasDisponibles = _aulaService.GetAll();
            return View(cursadaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CursadaCreateViewModel cursadaCreateViewModel)
        {
            var cursada = cursadaCreateViewModel.Cursada;
            var result = _cursadaService.Add(cursada);
            if (result == 1)
            {
               return RedirectToAction("Index");
            }
            else if (result == 0)
            {
                ModelState.AddModelError("Cursada.Aula", "Capacidad del aula excedida");
                return View(cursadaCreateViewModel);
            }
            else
            {
                ModelState.AddModelError("Cursada.Aula", "Capacidad del aula excedida");
                return View(cursadaCreateViewModel);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var cursada = _cursadaService.Get(id);
            return View(cursada);
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCursada(long id)
        {
            _cursadaService.Delete(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}