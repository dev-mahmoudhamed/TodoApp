using System;

namespace TodoApp.Dtos
{
    public class TodoInputFilter
    {
        public int? Status { get; set; }
        public int? Priority { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
