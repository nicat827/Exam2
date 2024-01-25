using Exam2.Entities;
using Exam2.Entities.Base;

namespace Exam2.Models
{
    public class Employee:BaseNamebleEntity
    {
        public string Surname { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? Bio { get; set; }
        public string? FacebookLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? LindekinLink { get; set; }
        //relational props
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; } = null!;
    }
}
