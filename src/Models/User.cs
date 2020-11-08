using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
  public class User
  {
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(60, ErrorMessage = "Name field should be contain between 3 and 60 characters")]
    [MinLength(3, ErrorMessage = "Name field should be contain between 3 and 60 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email field is invalid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MaxLength(100, ErrorMessage = "Password field should be contain between 6 and 100 characters")]
    [MinLength(6, ErrorMessage = "Password field should be contain between 6 and 100 characters")]
    public string Password { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}