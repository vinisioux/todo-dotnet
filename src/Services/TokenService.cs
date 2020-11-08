using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todo.Models;
using Microsoft.IdentityModel.Tokens;

namespace Todo.Services
{
  public class TokenService
  {
    public static string GenerateToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(Settings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Email, user.Email.ToString()),
          new Claim(ClaimTypes.Name, user.Name.ToString()),
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        }),
        Expires = DateTime.UtcNow.AddHours(9),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}