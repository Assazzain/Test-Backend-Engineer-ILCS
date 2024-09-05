using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; }

        [Required]
        [RegularExpression("pending|completed", ErrorMessage = "Status must be either 'pending' or 'completed'.")]
        public string Status { get; set; }
    }
}
