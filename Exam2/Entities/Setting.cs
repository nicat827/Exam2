using System.ComponentModel.DataAnnotations;

namespace Exam2.Entities
{
    public class Setting
    {
        [Key]
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;


    }
}
