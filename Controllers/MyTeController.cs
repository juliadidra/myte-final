using Microsoft.AspNetCore.Mvc;
using myte.Models;

namespace myte.Controllers
{
    public class MyTeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update()
        {          
            return View();
        }

    }
}
