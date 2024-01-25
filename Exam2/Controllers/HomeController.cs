using Exam2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Exam2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

      
    }
}