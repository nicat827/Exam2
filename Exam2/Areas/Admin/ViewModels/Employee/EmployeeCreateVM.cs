using Exam2.Entities;
using System.ComponentModel.DataAnnotations;

namespace Exam2.Areas.Admin.ViewModels
{
    public class EmployeeCreateVM
    {
        [MinLength(3, ErrorMessage = "Name must have minimum 3 length!")]
        [MaxLength(100, ErrorMessage = "Name must have maximum 100 length!")]
        public string Name { get; set; } = null!;

        [MinLength(3, ErrorMessage = "Surname must have minimum 3 length!")]
        [MaxLength(100, ErrorMessage = "Surname must have maximum 100 length!")]
        public string Surname { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Invalid input!")]
        public int ProfessionId { get; set; }

        public string? Bio { get; set; }
        public string? LinekdinLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? TwitterLink { get; set; }
        public IFormFile Photo { get; set; } = null!;

        public IEnumerable<Profession>? Professions { get; set; }
    }
}
