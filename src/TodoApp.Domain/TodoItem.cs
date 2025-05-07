using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;


namespace TodoApp
{
    public class TodoItem : AuditedAggregateRoot<Guid>
    {

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public TodoStatus Status { get; set; }
        public TodoPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }


        public void  MarkAsComplete()
        {
            Status = TodoStatus.Completed;
        }
        public void Update(
            string title,
            string? description,
            int status,
            int priority,
            DateTime? dueDate)
        {
            Title = title;
            Description = description;
            Status = (TodoStatus)status;
            Priority = (TodoPriority)priority;
            DueDate = dueDate;
        }
    }

    public enum TodoStatus
    {
        Pending,
        InProgress,
        Completed
    }

    public enum TodoPriority
    {
        Low,
        Medium,
        High
    }
}
