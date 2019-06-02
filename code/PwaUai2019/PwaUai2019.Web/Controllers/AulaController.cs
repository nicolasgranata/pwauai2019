using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PwaUai2019.Web.Models;
using PwaUai2019.Web.Repositories;
using PwaUai2019.Web.Services;

namespace PwaUai2019.Web.Controllers
{
    public class AulaController : Controller
    {
        private readonly IAulaService _aulaService;

        public AulaController(IAulaService aulaService)
        {
            _aulaService = aulaService;
        }

        public IActionResult Index()
        {
            IEnumerable<Aula> aulas = _aulaService.GetAll();
            return View(aulas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Aula aula)
        {
            _aulaService.Add(aula);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            return View(_aulaService.Get(id));
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAula(long id)
        {
            _aulaService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Get(long id)
        {
            return View("Detail", _aulaService.Get(id));
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            return View(_aulaService.Get(id));
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditAula(Aula aula)
        {
            _aulaService.Update(aula);

            return RedirectToAction("Index");
        }
    }
}