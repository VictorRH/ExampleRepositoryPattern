using ExampleRepositoryPattern.Core;
using ExampleRepositoryPattern.Core.Interfaz;
using Microsoft.AspNetCore.Mvc;

namespace ExampleRepositoryPattern.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly IGenericRepository<Teacher> repository;
        public TeacherController(IGenericRepository<Teacher> repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Teacher>>> GetAllTeachers()
        {
            var response = await repository.GetAllAsync();
            if (response.Count > 0)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacherId(int id)
        {
            var response = await repository.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<Teacher>> AddTeacher(Teacher teacher)
        {
            var response = await repository.Add(teacher);
            if (response == 0)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult<Teacher>> UpdateTeacher(int id, Teacher teacher)
        {
            var response = await repository.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            teacher.Id = id;
            var result = await repository.Update(teacher);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
