using Microsoft.AspNetCore.Mvc;

namespace File_Sharing.PL.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
