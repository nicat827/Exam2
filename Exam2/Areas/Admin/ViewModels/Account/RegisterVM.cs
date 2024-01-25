

using System.ComponentModel.DataAnnotations;

namespace Exam2.Areas.Admin.ViewModels
{
    public class RegisterVM
    {
        [MinLength(3, ErrorMessage = "Name length cant be less than 3!")]
        [MaxLength(100, ErrorMessage = "Name length cant be longer than 100!")]
        public string Name { get; set; } = null!;

        [MinLength(3, ErrorMessage = "Surname length cant be less than 3!")]
        [MaxLength(100, ErrorMessage = "Surname length cant be longer than 100!")]
        public string Surname { get; set; } = null!;


        [MinLength(5, ErrorMessage = "Username length cant be less than 5!")]
        [MaxLength(100, ErrorMessage = "Username length cant be longer than 100!")]
        public string UserName { get; set; } = null!;
    }
}
