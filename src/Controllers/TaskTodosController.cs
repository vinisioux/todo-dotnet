using System;
using System.Collections.Generic;
using System.Linq;
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
  public class TaskTodosController : Controller
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

      taskTodo.UserId = new Guid(user_id);
      taskTodo.User = user;
      taskTodo.Progress = "to do";
      taskTodo.CreatedAt = DateTime.Now;
      taskTodo.UpdatedAt = DateTime.Now;
      taskTodo.Title.Trim();
      taskTodo.Description.Trim();

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

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<TaskTodo>>> Index([FromServices] DataContext context)
    {
      var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);

      var taskTodos = await context.TaskTodos
      .Include(tt => tt.User)
      .AsNoTracking()
      .Where(u => u.UserId.ToString() == user_id)
      .ToListAsync();

      return taskTodos;
    }

    [HttpGet]
    [Authorize]
    [Route("{id}")]
    public async Task<ActionResult<TaskTodo>> Find([FromServices] DataContext context, [FromRoute] string id)
    {
      var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);

      var taskTodo = await context.TaskTodos
      .Include(tt => tt.User)
      .AsNoTracking()
      .Where(u => u.UserId.ToString() == user_id)
      .Where(tt => tt.Id.ToString() == id)
      .FirstOrDefaultAsync();

      return taskTodo;
    }

    [HttpPut]
    [Authorize]
    [Route("{id}")]
    public async Task<ActionResult<TaskTodo>> Update([FromServices] DataContext context, [FromRoute] string id, [FromBody] TaskTodo updatedTasktodo)
    {

      if (updatedTasktodo.Progress != "to do" && updatedTasktodo.Progress != "doing" && updatedTasktodo.Progress != "done")
      {
        return BadRequest(error: "Progress should be contain only: 'to do', 'doing' or 'done' ");
      }

      var taskTodo = await context.TaskTodos.FirstOrDefaultAsync(u => u.Id.ToString() == id);

      taskTodo.Title = updatedTasktodo.Title.Trim();
      taskTodo.Description = updatedTasktodo.Description.Trim();
      taskTodo.Progress = updatedTasktodo.Progress;
      taskTodo.UpdatedAt = DateTime.Now;

      context.TaskTodos.Update(taskTodo);
      await context.SaveChangesAsync();

      return taskTodo;
    }

    [HttpDelete]
    [Authorize]
    [Route("{id}")]
    public async Task<ActionResult<dynamic>> Delete([FromServices] DataContext context, [FromRoute] string id)
    {
      try
      {
        var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var taskTodo = await context.TaskTodos
              .Include(tt => tt.User)
              .AsNoTracking()
              .Where(u => u.UserId.ToString() == user_id)
              .Where(tt => tt.Id.ToString() == id)
              .FirstOrDefaultAsync();

        context.TaskTodos.Remove(taskTodo);
        await context.SaveChangesAsync();

        return new
        {
          message = "task successfully deleted"
        };
      }
      catch (System.Exception err)
      {
        return BadRequest(error: err);
      }
    }
  }
}