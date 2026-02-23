using Microsoft.AspNetCore.Mvc;

namespace AllinOne.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
