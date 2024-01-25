using Exam2.Areas.Admin.ViewModels;
using Exam2.DAL;
using Exam2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Exam2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private const int LIMIT = 5;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) return NotFound();

            int count = await _context.Settings.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / LIMIT);
            IEnumerable<Setting> settings = await _context.Settings.Skip((page - 1) * LIMIT).Take(LIMIT).ToListAsync();
            return View(new PaginationVM<Setting> { CurrentPage = page, TotalPages = totalPages, Items = settings });
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return NotFound();
            Setting? setting = await _context.Settings.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (setting is null) return BadRequest();
            return View(new SettingUpdateVM { Value = setting.Value});

        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, SettingUpdateVM vm)
        {
            Setting? setting = await _context.Settings.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (setting is null) return BadRequest();
            
            setting.Value = vm.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
