using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi_Project.Models;

namespace WebApi_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("index-all")]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            var classObject = new Class { Id = 1, Module = "5th", Name = "Turma01" };
            

            var students = new List<Student>
            {
                new Student { 
                    Id = 1, 
                    Name = "Leonardo Oliveira", 
                    Birth_date = "15/10/1996", 
                    Email = "leo@email.com",
                    Class = classObject,
                    ClassId = 1, 
                    Hobby = "Nadar" 
                }
            };

            return Ok(students);
        }
    }
}
    