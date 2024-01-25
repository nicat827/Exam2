using Exam2.Areas.Admin.ViewModels;
using Exam2.DAL;
using Exam2.Entities;
using Exam2.Models;
using Exam2.Utilities.Enums;
using Exam2.Utilities.Extencions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Exam2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EmployeeController:Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private const int LIMIT = 5;
        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page <1) return NotFound();

            int count = await _context.Employees.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / LIMIT);
            IEnumerable<Employee> employees = await _context.Employees.Skip((page -1) * LIMIT).Take(LIMIT).ToListAsync();
            return View(new PaginationVM<Employee> { CurrentPage = page ,TotalPages = totalPages, Items = employees });
        }

        public async Task<IActionResult> Create()
        {
            return View(new EmployeeCreateVM { Professions = await _getProfessions() });

        }

        [HttpPost]

        public async Task<IActionResult> Create(EmployeeCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Professions = await _getProfessions();
                return View(vm);
            }
            if (!vm.Photo.CheckFileType(FileType.Image))
            {
                ModelState.AddModelError("Photo", "Please, make sure, you uploaded a photo");
                vm.Professions = await _getProfessions();
                return View(vm);
            }

            if (!vm.Photo.CheckFileSize(2, SizeType.Mb))
            {
                ModelState.AddModelError("Photo", $"{nameof(vm.Photo)} size cant be longer than 2MB!");
                vm.Professions = await _getProfessions();
                return View(vm);
            }

            if (!await _context.Professions.AnyAsync(p => p.Id == vm.ProfessionId))
            {
                vm.Professions = await _getProfessions();
                ModelState.AddModelError("ProfessionId", "Profession with this name already exists!");
                return View(vm);
            }

            Employee employee = new Employee
            {
                Name = vm.Name.Capitalize(),
                Surname = vm.Surname.Capitalize(),
                Bio = vm.Bio,
                FacebookLink = vm.FacebookLink,
                TwitterLink = vm.TwitterLink,
                InstagramLink = vm.InstagramLink,
                LindekinLink = vm.LinekdinLink,
                ProfessionId = vm.ProfessionId,
                ImageUrl = await vm.Photo.CreateFileAsync(_env.WebRootPath, "uploads", "employees"),
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<Profession>> _getProfessions()
        {
            return await _context.Professions.ToListAsync();
        }
       
    }
}
