using Exam2.Entities;
using Exam2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exam2.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt): base(opt) { }
        

        public DbSet<Profession> Professions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
