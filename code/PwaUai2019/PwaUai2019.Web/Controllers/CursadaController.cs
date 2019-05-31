using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PwaUai2019.Web.Models;
using PwaUai2019.Web.Repositories;

namespace PwaUai2019.Web.Controllers
{
    public class CursadaController : Controller
    {
        public IActionResult Index()
        {
            List<Cursada> cursadas = new List<Cursada>();
            return View(cursadas);
        }

        public IActionResult Create()
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