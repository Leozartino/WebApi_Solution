using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using WebApi_Project.Context;
using WebApi_Project.Dto;
using WebApi_Project.Models;
using WebApi_Project.Services;

namespace WebApi_Project.Controllers
{
  [Route("api/auth-controller")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly AppDbContext _appDbContext;

    public AuthController(AppDbContext appDbContext)
    {
      _appDbContext = appDbContext;
    }


    [HttpPost]
    [Route("login")]
    public ActionResult<dynamic> LoginUser(UserLoginDto userLogin)
    {
      var user = _appDbContext.Users.Where((User u) => u.Username == userLogin.Username).FirstOrDefault(); ;



      if (user == null)
      {
        return NotFound(new { message = "User not found!" });
      }


      if (!VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
      {
        return BadRequest(new { message = "Username and/or password are incorrect!" });
      }

      //Ger o token
      var token = TokenService.GenerateToken(user);

      var userResponse = new
      {
        user.Id,
        user.Name,
        user.Username,
        user.Role,
      };

      return new
      {
        user = userResponse,
        token,
      };

    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      using (var hmac = new HMACSHA512(passwordSalt))
      {
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return hash.SequenceEqual(passwordHash);
      };


    }


  }
}
