using ExampleRepositoryPattern.Core;
using ExampleRepositoryPattern.Core.Interfaz;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleRepositoryPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IGenericRepository<Student> repository;

        public StudentsController(IGenericRepository<Student> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Student>>> GetALLStudents()
        {
            var response = await repository.GetAllAsync();
            if (response.Count > 0)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentId(int id)
        {
            var response = await repository.GetByIdAsync(id);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            var response = await repository.Add(student);
            if (response == 0)
            {
                BadRequest();
            }
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student student)
        {
            
            student.Id = id;
            var result = await repository.Update(student);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }



    }
}
