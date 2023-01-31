using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class WebController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}