using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi_Project.Context;
using WebApi_Project.Models;
using WebApi_Project.Services;

namespace WebApi_Project.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public LoginController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("login/{id}")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync(int id)
        {
            //Recuperar o person
            var professor = await _appDbContext.Professors.FindAsync(id);

            if(professor == null)
            {
                return NotFound(new { message = "professor not found!" });
            }

            //Ger o token
            var token = TokenService.GenerateToken(professor);


            return new
            {
                user = professor,
                token,
            };
        }
        
    }
}
