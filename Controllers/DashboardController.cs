using Microsoft.AspNetCore.Mvc;

namespace myte.Controllers
{
    public class DashboardController : Controller
    {
       

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
      
    }
}
