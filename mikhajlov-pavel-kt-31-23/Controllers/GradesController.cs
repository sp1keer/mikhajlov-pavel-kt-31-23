using Microsoft.AspNetCore.Mvc;
using mikhajlov_pavel_kt_31_23.Filters;
using mikhajlov_pavel_kt_31_23.Models;
using mikhajlov_pavel_kt_31_23.Services.Interfaces;

namespace mikhajlov_pavel_kt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradesController(IGradeService gradeService) => _gradeService = gradeService;

        [HttpGet]
        public async Task<ActionResult<Grade[]>> GetGrades([FromQuery] GradeFilter filter, CancellationToken cancellationToken)
        {
            var grades = await _gradeService.GetGradesAsync(filter ?? new GradeFilter(), cancellationToken);
            return Ok(grades);
        }

        [HttpPost]
        public async Task<ActionResult<Grade>> AddGrade([FromBody] Grade grade, CancellationToken cancellationToken)
        {
            var created = await _gradeService.AddGradeAsync(grade, cancellationToken);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Grade>> UpdateGrade(int id, [FromBody] Grade grade, CancellationToken cancellationToken)
        {
            var updated = await _gradeService.UpdateGradeAsync(id, grade, cancellationToken);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }
}
