using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi_Project.Models;

namespace WebApi_Project.Controllers
{
    
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private static readonly List<User> users = new()
        {
                new User {
                    Id = 1,
                    Name = "Leonardo Oliveira",
                    Email = "leo@email.com",
                    Password = "8420@xkl",
                    IsAdmin = true,
                },
                new User {
                    Id = 2,
                    Name = "Maria Silva",
                    Email = "maria@email.com",
                    Password = "123@kcx",
                    IsAdmin = true,
                },
                new User
                {
                    Id = 3,
                    Name = "João Pereira",
                    Email = "joao@email.com",
                    Password = "456@xkl",
                    IsAdmin = false,
                }
            };

        [HttpGet]
        [Route("index-all")]
        //o c# não suporta conversão Implicita de operadores em Interface
        // Entao voce pode usar List(concreto) ou um IEnumerable mas no final
        // usar o .ToList() se caso eu usar um dbContext
        public ActionResult<IEnumerable<User>> IndexAllUsers()
        {

            return Ok(users);
        }
          
        [HttpGet]
        [Route("index-one/{id}")]
        public ActionResult<User> IndexOneUser(int id)
        {
            var user = users.Find((User user) => user.Id == id);

            if (user == null)
            {
                return NotFound(new { message = "user not found!" });
            }

            return Ok(user);
        }


        [HttpPost]
        [Route("create-user")]
        public ActionResult<User> CreateUser([FromBody]User user)
        {
            User userRequest = user;
            users.Add(userRequest);

            return Created("~api/User/create-user", userRequest);
        }

        [HttpPut]
        [Route("update-user/{id}")]
        public ActionResult<User> UpdateUser(User request, int id)
        {
            var user = users.Find((User user) => user.Id == id);

            if(user == null)
            {
                return NotFound(new { message = "user not found!" });
            }

            user.Name = request.Name;
            user.Email = request.Email;
            user.Password = request.Password;


            return Ok(user);


        }

        [HttpDelete]
        [Route("delete-user/{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            var user = users.Find((User user) => user.Id == id);

            if (user == null)
            {
                return NotFound(new { message = "user not found!" });
            }

            users.Remove(user);

            return Ok( new { message = "user removed sucessfully!"});


        }
    }
}
