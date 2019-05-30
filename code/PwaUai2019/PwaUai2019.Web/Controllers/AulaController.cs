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
        private readonly IRepository<Aula> _nodeProvider;

        public AulaController(IRepository<Aula> nodeProvider)
        {
            _nodeProvider = nodeProvider;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}