using Microsoft.AspNetCore.Mvc;

namespace Exam2.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
