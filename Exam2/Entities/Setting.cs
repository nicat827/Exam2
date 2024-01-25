using Exam2.Entities.Base;

namespace Exam2.Entities
{
    public class Setting:BaseEntity
    {
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;


    }
}
