using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PwaUai2019.Web.Models;
using PwaUai2019.Web.Repositories;

namespace PwaUai2019.Web.Controllers
{
    public class AulaController : Controller
    {
        private readonly AulaRepository _aulaRepository;

        public AulaController(AulaRepository aulaRepository)
        {
            _aulaRepository = aulaRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Aula> aulas = _aulaRepository.GetAll();
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
            _aulaRepository.Add(aula);
            return View();
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var aula = _aulaRepository.Get(id);
            return View(aula);
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAula(long id)
        {
            _aulaRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }
}