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
                ModelState.AddModelError("ProfessionId", "Profession with this name doesnt exists!");
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
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return NotFound();
            Employee? employee = await _context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (employee is null) return BadRequest();
            return View(new EmployeeUpdateVM {
                Professions = await _getProfessions(),
                Name = employee.Name,
                Surname = employee.Surname,
                Bio = employee.Bio,
                ImageUrl = employee.ImageUrl,
                FacebookLink = employee.FacebookLink,
                TwitterLink = employee.TwitterLink,
                LinekdinLink = employee.LindekinLink,
                InstagramLink = employee.InstagramLink,
                ProfessionId = employee.ProfessionId,
            });

        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, EmployeeUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Professions = await _getProfessions();
                return View(vm);
            }

            Employee? employee = await _context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (employee is null) return BadRequest();
            if (vm.ProfessionId != employee.ProfessionId)
            {
                if (!await _context.Professions.AnyAsync(p => p.Id == vm.ProfessionId))
                {
                    vm.Professions = await _getProfessions();
                    ModelState.AddModelError("ProfessionId", "Profession with this name doesnt exists!");
                    return View(vm);
                }

                employee.ProfessionId = vm.ProfessionId;
            }
            if (vm.Photo is not null)
            {
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

                employee.ImageUrl.DeleteFile(_env.WebRootPath, "uploads", "employees");
                employee.ImageUrl = await vm.Photo.CreateFileAsync(_env.WebRootPath, "uploads", "employees");

            }


            employee.Name = vm.Name.Capitalize();
            employee.Surname = vm.Surname.Capitalize();
            employee.Bio = vm.Bio;
            employee.FacebookLink = vm.FacebookLink;
            employee.TwitterLink = vm.TwitterLink;
            employee.InstagramLink = vm.InstagramLink;
            employee.LindekinLink = vm.LinekdinLink;
          
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return NotFound();
            Employee? employee = await _context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (employee is null) return BadRequest();
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // get professions curstom method
        private async Task<IEnumerable<Profession>> _getProfessions()
        {
            return await _context.Professions.ToListAsync();
        }
       
    }
}
