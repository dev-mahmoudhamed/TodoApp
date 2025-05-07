using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos
{
    public class TodoItemInputDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
      
        public int Status { get; set; }

        public int Priority { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
