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

        [MinLength(6, ErrorMessage = "Email length cant be less than 6!")]
        [MaxLength(256, ErrorMessage = "Email length cant be longer than 256!")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Incorrect email!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [MinLength(8, ErrorMessage = "Password length cant be less than 8!")]
        [MaxLength(100, ErrorMessage = "Password length cant be longer than 100!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [MinLength(8, ErrorMessage = "Password length cant be less than 8!")]
        [MaxLength(100, ErrorMessage = "Password length cant be longer than 100!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords mismatch!")]
        public string RepeatPassword { get; set; } = null!;
    }
}
