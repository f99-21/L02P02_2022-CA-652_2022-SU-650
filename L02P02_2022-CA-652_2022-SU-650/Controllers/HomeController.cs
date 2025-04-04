using System.Diagnostics;
using L02P02_2022_CA_652_2022_SU_650.Models;
using Microsoft.AspNetCore.Mvc;

namespace L02P02_2022_CA_652_2022_SU_650.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout() 
        {
            HttpContext.Session.Clear();

            return RedirectToAction("agregarCliente", "clientes");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
