using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi_Project.Context;
using WebApi_Project.Models;

namespace WebApi_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController: ControllerBase

    {
        private readonly AppDbContext _appDbContext;
        
        public ProfessorController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("index-all")]
        public async Task<ActionResult<IEnumerable<Professor>>> GetProfessors()
        {
            return Ok(await _appDbContext.Professors.ToListAsync());

        }


    }
}
