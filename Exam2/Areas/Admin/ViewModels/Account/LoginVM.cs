using System.ComponentModel.DataAnnotations;

namespace Exam2.Areas.Admin.ViewModels
{
    public class LoginVM
    {
        [MinLength(5, ErrorMessage = "This field length cant be less than 5!")]
        [MaxLength(256, ErrorMessage = "This field length cant be longer than 256!")]
        public string UserNameOrEmail { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Password length cant be longer than 100!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool IsPersistence { get; set; } 
    }
}
