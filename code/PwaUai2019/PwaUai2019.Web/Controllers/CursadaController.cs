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
            return View(cursadaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CursadaCreateViewModel cursadaCreateViewModel)
        {
            var cursada = cursadaCreateViewModel.Cursada;

            _cursadaService.Add(cursada);

            return RedirectToAction("Index");

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCursada(long id)
        {
            _cursadaService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Get(long id)
        {
            return View("Detail", _aulaService.Get(id));
        }

        [HttpGet]
        public IActionResult Assign()
        {
            _cursadaService.AssignAula();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetNoAula()
        {
            return View("NoAula", _cursadaService.GetAllWithoutAula());
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var cursadaEditViewModel = new CursadaEditViewModel()
            {
                Cursada = _cursadaService.Get(id)
            };

            return View(cursadaEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCursada(Cursada cursada)
        {
            _cursadaService.Update(cursada);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}