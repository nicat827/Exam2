using Exam2.Entities;
using Exam2.Entities.Base;

namespace Exam2.Areas.Admin.ViewModels
{
    public class PaginationVM<T> where T : BaseEntity
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<T> Items = new List<T>();
    }
}
