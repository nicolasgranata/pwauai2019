using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetGain;
using PwaUai2019.Web.Models;
using PwaUai2019.Web.Repositories;

namespace PwaUai2019.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Alumno> _nodeProvider;

        public HomeController(IRepository<Alumno> nodeProvider)
        {
            _nodeProvider = nodeProvider;
        }
        public IActionResult Index()
        {
            Alumno alumno = new Alumno() { Legajo = 1234, Nombre = "Fernando", Apellido = "Perez" };

            _nodeProvider.Add(alumno);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
