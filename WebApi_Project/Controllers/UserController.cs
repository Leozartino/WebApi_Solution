using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi_Project.Models;

namespace WebApi_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public ActionResult<IEnumerable<User>> GetAllUser()
        {

            return Ok(users);
        }

        [HttpPost]
        [Route("post-user")]
        public ActionResult<User> PostUser([FromBody]User user)
        {
            users.Add(user);
            return Ok();

        }
    }
}
