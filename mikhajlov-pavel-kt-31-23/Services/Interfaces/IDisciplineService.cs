using Microsoft.EntityFrameworkCore;
using mikhajlov_pavel_kt_31_23.Filters;
using mikhajlov_pavel_kt_31_23.Models;
using mikhajlov_pavel_kt_31_23.Database;

namespace mikhajlov_pavel_kt_31_23.Services.Interfaces
{
    public interface IDisciplineService
    {
        Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken);
        Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken);
        Task<Discipline?> UpdateDisciplineAsync(int id, Discipline discipline, CancellationToken cancellationToken);
        Task DeleteDisciplineAsync(int id, CancellationToken cancellationToken);
    }

    public class DisciplineService : IDisciplineService
    {
        private readonly StudentDbContext _dbContext;
        public DisciplineService(StudentDbContext dbContext) => _dbContext = dbContext;

        public async Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken)
        {
            IQueryable<Discipline> query = _dbContext.Disciplines;
            if (filter.IsDeleted.HasValue) query = query.Where(d => d.IsDeleted == filter.IsDeleted.Value);
            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken)
        {
            discipline.IsDeleted = false;
            _dbContext.Disciplines.Add(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return discipline;
        }

        public async Task<Discipline?> UpdateDisciplineAsync(int id, Discipline discipline, CancellationToken cancellationToken)
        {
            var existing = await _dbContext.Disciplines.FindAsync(id);
            if (existing == null) return null;

            existing.Name = discipline.Name;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task DeleteDisciplineAsync(int id, CancellationToken cancellationToken)
        {
            var discipline = await _dbContext.Disciplines.FindAsync(id);
            if (discipline == null) return;

            discipline.IsDeleted = true; // Soft delete дисциплины
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
