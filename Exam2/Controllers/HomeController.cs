using Exam2.DAL;
using Exam2.Entities;
using Exam2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Exam2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Employee> employees = await _context.Employees.Include(e => e.Profession).ToListAsync();
            return View(employees);
        }

      
    }
}