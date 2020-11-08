using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
  [Route("v1/users")]
  public class UsersController : Controller
  {
    [HttpPost]
    public async Task<ActionResult<dynamic>> Store([FromServices] DataContext context, [FromBody] User user)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var userExists = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

      if (userExists == null)
      {
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new
        {
          user.Id,
          user.Name,
          user.Email,
          user.CreatedAt,
          user.UpdatedAt
        };
      }
      else
      {
        return BadRequest(error: "User already exists");
      }
    }
  }
}