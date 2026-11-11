using Microsoft.AspNetCore.Mvc;
using mikhajlov_pavel_kt_31_23.Filters;
using mikhajlov_pavel_kt_31_23.Models;
using mikhajlov_pavel_kt_31_23.Services.Interfaces;

namespace mikhajlov_pavel_kt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplineService _disciplineService;
        public DisciplinesController(IDisciplineService disciplineService) => _disciplineService = disciplineService;

        [HttpGet]
        public async Task<ActionResult<string>> GetDisciplines([FromQuery] DisciplineFilter filter, CancellationToken cancellationToken)
        {
            var disciplines = await _disciplineService.GetDisciplinesAsync(filter ?? new DisciplineFilter(), cancellationToken);
            var result = string.Join(", ", disciplines.Where(d => !string.IsNullOrEmpty(d.Name)).Select(d => d.Name));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Discipline>> AddDiscipline([FromBody] Discipline discipline, CancellationToken cancellationToken)
        {
            var created = await _disciplineService.AddDisciplineAsync(discipline, cancellationToken);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Discipline>> UpdateDiscipline(int id, [FromBody] Discipline discipline, CancellationToken cancellationToken)
        {
            var updated = await _disciplineService.UpdateDisciplineAsync(id, discipline, cancellationToken);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscipline(int id, CancellationToken cancellationToken)
        {
            await _disciplineService.DeleteDisciplineAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
