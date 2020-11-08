using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
  [Route("v1/tasks")]
  public class TasksController : Controller
  {
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<dynamic>> Store([FromServices] DataContext context, [FromBody] TaskTodo taskTodo)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);

      var user = await context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == user_id);

      user.Password = "";

      taskTodo.UserId = user_id;
      taskTodo.User = user;
      taskTodo.Progress = "to do";
      taskTodo.CreatedAt = DateTime.Now;
      taskTodo.UpdatedAt = DateTime.Now;

      context.TaskTodos.Add(taskTodo);
      await context.SaveChangesAsync();

      return new
      {
        taskTodo.Id,
        taskTodo.Title,
        taskTodo.Description,
        taskTodo.Progress,
        taskTodo.User,
        taskTodo.CreatedAt,
        taskTodo.UpdatedAt,
      };
    }
  }
}