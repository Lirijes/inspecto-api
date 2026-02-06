using Microsoft.AspNetCore.Mvc;

namespace inspecto_API.Controllers
{
    public class CaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
