using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi_Project.Context;
using WebApi_Project.Models;

namespace WebApi_Project.Controllers
{
    [ApiController]
    [Route("api/professors")]
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

        [HttpGet]
        [Route("index-one/{id}")]
        public async Task<ActionResult<Professor>> IndexOneProfessor(int id)
        {
            var professor = await _appDbContext.Professors.FindAsync(id);

            if (professor == null)
            {
                return NotFound(new { message = "professor not found!" });
            }

            return Ok(professor);
        }


        [HttpPost]
        [Route("create-professor")]
        public async Task<ActionResult<Professor>> CreateProfessor([FromBody] Professor professor)
        {
            var classObject = await _appDbContext.Classes.FindAsync(professor.ClassId);

            if (classObject == null)
            {
                return NotFound(new { message = "class not found!" });
            }

            var professorObj = new Professor 
            { 
                Id = professor.Id, 
                Name = professor.Name, 
                Birth_date = professor.Birth_date, 
                Email = professor.Email, 
                ClassId = professor.ClassId, 
                Specialization = professor.Specialization
            };

            _appDbContext.Professors.Add(professorObj);
            await _appDbContext.SaveChangesAsync();
            
            return Created("~api/professors/create-professor", professorObj);
        }

        [HttpPut]
        [Route("update-professor/{id}")]
        public async Task<ActionResult<Professor>> UpdateProfessor(Professor request, int id)
        {
            var professor = await _appDbContext.Professors.FindAsync(id);

            if (professor == null)
            {
                return NotFound(new { message = "professor not found!" });
            }

            professor.Name = request.Name;
            professor.Email = request.Email;
            professor.Birth_date = request.Birth_date;
            professor.Specialization = request.Specialization;

            var classObject = await _appDbContext.Classes.FindAsync(request.ClassId);

            if(classObject == null)
            {
                return NotFound(new {message = "class does not exist!"});
            }

            professor.ClassId = request.ClassId;

            await _appDbContext.SaveChangesAsync();


            return Ok(professor);


        }

        [HttpDelete]
        [Route("delete-professor/{id}")]
        public async Task<ActionResult> DeleteProfessor(int id)
        {
            var professor = await _appDbContext.Professors.FindAsync(id);

            if (professor == null)
            {
                return NotFound(new { message = "professor not found!" });
            }

            _appDbContext.Professors.Remove(professor);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { message = "professor removed sucessfully!" });


        }
    }


}

