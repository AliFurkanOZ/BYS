using Microsoft.AspNetCore.Mvc;

namespace BYS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult KTU()
        {
            return View();
        }
    }
}
