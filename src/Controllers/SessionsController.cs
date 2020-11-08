using System.Threading.Tasks;
using Dtos.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Services;

namespace Todo.Controllers
{
  [Route("v1/sessions")]
  public class SessionsController : Controller
  {
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Store([FromBody] UserSession user, [FromServices] DataContext context)
    {
      var userExists = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

      if (userExists == null)
      {
        return BadRequest(error: "Email/Password does not match");
      }

      var passwordIsValid = BCrypt.Net.BCrypt.Verify(user.Password, userExists.Password);

      if (!passwordIsValid)
      {
        return BadRequest(error: "Email/Password does not match");
      }

      var token = TokenService.GenerateToken(userExists);

      userExists.Password = "";

      return new
      {
        user = userExists,
        token,
      };
    }
  }
}