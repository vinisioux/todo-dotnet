using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
  public class TaskTodo
  {
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(40, ErrorMessage = "Title field should be contain between 3 and 40 characters")]
    [MinLength(3, ErrorMessage = "Title field should be contain between 3 and 40 characters")]
    public string Title { get; set; }

    [MaxLength(255, ErrorMessage = "Description field should be contain until 255 characters")]
    public string Description { get; set; }

    public string Progress { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}