using Microsoft.AspNetCore.Mvc;
using mikhajlov_pavel_kt_31_23.Filters;
using mikhajlov_pavel_kt_31_23.Models;
using mikhajlov_pavel_kt_31_23.Services.Interfaces;

namespace mikhajlov_pavel_kt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IStudentDisciplineService _studentDisciplineService;

        public StudentsController(
            IStudentService studentService,
            IStudentDisciplineService studentDisciplineService)
        {
            _studentService = studentService;
            _studentDisciplineService = studentDisciplineService;
        }

        [HttpGet]
        public async Task<ActionResult<Student[]>> GetStudents([FromQuery] StudentFilter filter, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetStudentsAsync(filter ?? new StudentFilter(), cancellationToken);
            return Ok(students);
        }

        [HttpGet("disciplines")]
        public async Task<ActionResult<string[]>> GetDisciplinesByFirstName([FromQuery] string firstName, CancellationToken cancellationToken)
        {
            var disciplines = await _studentDisciplineService.GetDisciplinesByFirstNameAsync(firstName, cancellationToken);
            return Ok(disciplines);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent([FromBody] Student student, CancellationToken cancellationToken)
        {
            var created = await _studentService.AddStudentAsync(student, cancellationToken);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, [FromBody] Student student, CancellationToken cancellationToken)
        {
            var updated = await _studentService.UpdateStudentAsync(id, student, cancellationToken);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id, CancellationToken cancellationToken)
        {
            await _studentService.DeleteStudentAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
