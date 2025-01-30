using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoapp.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        [RegularExpression("^(Low|Medium|High)$", ErrorMessage = "Invalid priority value")]
        public string Priority { get; set; } = "Medium";
    }
}
