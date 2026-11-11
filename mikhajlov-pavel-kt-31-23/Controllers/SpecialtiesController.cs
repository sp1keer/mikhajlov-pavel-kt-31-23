using Microsoft.AspNetCore.Mvc;
using mikhajlov_pavel_kt_31_23.Filters;
using mikhajlov_pavel_kt_31_23.Models;
using mikhajlov_pavel_kt_31_23.Services.Interfaces;

namespace mikhajlov_pavel_kt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;
        public SpecialtiesController(ISpecialtyService specialtyService) => _specialtyService = specialtyService;

        [HttpGet]
        public async Task<ActionResult<Specialty[]>> GetSpecialties([FromQuery] SpecialtyFilter filter, CancellationToken cancellationToken)
        {
            var specialties = await _specialtyService.GetSpecialtiesAsync(filter ?? new SpecialtyFilter(), cancellationToken);
            return Ok(specialties);
        }

        [HttpPost]
        public async Task<ActionResult<Specialty>> AddSpecialty([FromBody] Specialty specialty, CancellationToken cancellationToken)
        {
            var created = await _specialtyService.AddSpecialtyAsync(specialty, cancellationToken);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Specialty>> UpdateSpecialty(int id, [FromBody] Specialty specialty, CancellationToken cancellationToken)
        {
            var updated = await _specialtyService.UpdateSpecialtyAsync(id, specialty, cancellationToken);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }
}
