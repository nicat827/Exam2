using Exam2.DAL;
using Microsoft.EntityFrameworkCore;

namespace Exam2.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            return await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
        }
    }
}
