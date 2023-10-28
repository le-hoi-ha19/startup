using Microsoft.AspNetCore.Mvc;

namespace startup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        // hiển thị ra trang admin
        public IActionResult Index()
        {
            return View();
        }
    }
}
