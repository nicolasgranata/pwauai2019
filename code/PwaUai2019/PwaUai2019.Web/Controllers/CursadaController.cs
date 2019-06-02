using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PwaUai2019.Web.Models;
using PwaUai2019.Web.Repositories;
using PwaUai2019.Web.Services;
using PwaUai2019.Web.ViewModels;

namespace PwaUai2019.Web.Controllers
{
    public class CursadaController : Controller
    {
        private readonly CursadaRepository _cursadaRepository;
        private readonly AulaRepository _aulaRepository;
        private readonly ICursadaService _cursadaService;

        public CursadaController(ICursadaService cursadaService, CursadaRepository cursadaRepository, AulaRepository aulaRepository)
        {
            _cursadaRepository = cursadaRepository;
            _aulaRepository = aulaRepository;
            _cursadaService = cursadaService;
        }

        public IActionResult Index()
        {
            IEnumerable<Cursada> cursadas = _cursadaRepository.GetAll();
            return View(cursadas);
        }

        public IActionResult Create()
        {
            var cursadaViewModel = new CursadaCreateViewModel();
            cursadaViewModel.AulasDisponibles = _aulaRepository.GetAll();
            return View(cursadaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CursadaCreateViewModel cursadaCreateViewModel)
        {
            var cursada = cursadaCreateViewModel.Cursada;
            var result = _cursadaService.CreateCursada(cursada);
            if (result == 1)
            {
               return RedirectToAction("Index");
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
            var cursada = _cursadaRepository.Get(id);
            return View(cursada);
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCursada(long id)
        {
            _cursadaRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}