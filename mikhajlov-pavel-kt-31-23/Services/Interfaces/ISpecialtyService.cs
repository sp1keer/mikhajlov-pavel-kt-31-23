using Microsoft.EntityFrameworkCore;
using mikhajlov_pavel_kt_31_23.Database;
using mikhajlov_pavel_kt_31_23.Filters;
using mikhajlov_pavel_kt_31_23.Models;

namespace mikhajlov_pavel_kt_31_23.Services.Interfaces
{
    public interface ISpecialtyService
    {
        Task<Specialty[]> GetSpecialtiesAsync(SpecialtyFilter filter, CancellationToken cancellationToken);
        Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken);
        Task<Specialty?> UpdateSpecialtyAsync(int id, Specialty specialty, CancellationToken cancellationToken);
    }

    public class SpecialtyService : ISpecialtyService
    {
        private readonly StudentDbContext _dbContext;
        public SpecialtyService(StudentDbContext dbContext) => _dbContext = dbContext;

        public async Task<Specialty[]> GetSpecialtiesAsync(SpecialtyFilter filter, CancellationToken cancellationToken)
        {
            IQueryable<Specialty> query = _dbContext.Specialtys;

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(s => s.Title.Contains(filter.Title));
            if (!string.IsNullOrWhiteSpace(filter.Code))
                query = query.Where(s => s.Code.Contains(filter.Code));

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken)
        {
            _dbContext.Specialtys.Add(specialty);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return specialty;
        }

        public async Task<Specialty?> UpdateSpecialtyAsync(int id, Specialty specialty, CancellationToken cancellationToken)
        {
            var existing = await _dbContext.Specialtys.FindAsync(id);
            if (existing == null) return null;

            existing.Title = specialty.Title;
            existing.Code = specialty.Code;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return existing;
        }
    }
}
