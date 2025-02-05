using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models{
    public class TodoItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly DueDate {get; set;}
        public DateOnly CreationDate {get; set;}
        public int Priority {get; set;}
        public bool IsDone { get; set; }
    }
}
