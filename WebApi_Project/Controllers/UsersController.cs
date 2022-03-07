using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebApi_Project.Context;
using WebApi_Project.Dto;
using WebApi_Project.Models;

namespace WebApi_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly AppDbContext _appDbContext;

        public UsersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("create-user")]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserDto userDto)
        {
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            Guid id = Guid.NewGuid();


            User user = new ()
            {
                Id = id.ToString(),
                Username = userDto.Username,
                Email = userDto.Email,
                Name = userDto.Name,
                Role = "normal",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

             _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpGet]
        [Route("index-one/{id}")]
        public async Task<ActionResult<User>> IndexOneUser(int id)
        {
            var user = await _appDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "user not found!" });
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("index-all")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _appDbContext.Users.ToListAsync());
        }
  

        [HttpPut]
        [Route("update-user/{id}")]
        public async Task<ActionResult<User>> UpdateUser(User request, int id)
        {
            var user = await _appDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "user not found!" });
            }

            user.Name = request.Name;
            user.Email = request.Email;

            await _appDbContext.SaveChangesAsync();


            return Ok(user);


        }

        [HttpDelete]
        [Route("delete-user/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _appDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "user not found!" });
            }

            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { message = "user removed sucessfully!" });


        }



        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
