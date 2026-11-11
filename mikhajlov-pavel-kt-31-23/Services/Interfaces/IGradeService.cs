using Microsoft.EntityFrameworkCore;
using mikhajlov_pavel_kt_31_23.Database;
using mikhajlov_pavel_kt_31_23.Filters;
using mikhajlov_pavel_kt_31_23.Models;

namespace mikhajlov_pavel_kt_31_23.Services.Interfaces
{
    public interface IGradeService
    {
        Task<Grade[]> GetGradesAsync(GradeFilter filter, CancellationToken cancellationToken);
        Task<Grade> AddGradeAsync(Grade grade, CancellationToken cancellationToken);
        Task<Grade?> UpdateGradeAsync(int id, Grade grade, CancellationToken cancellationToken);
    }

    public class GradeService : IGradeService
    {
        private readonly StudentDbContext _dbContext;
        public GradeService(StudentDbContext dbContext) => _dbContext = dbContext;

        public async Task<Grade[]> GetGradesAsync(GradeFilter filter, CancellationToken cancellationToken)
        {
            IQueryable<Grade> query = _dbContext.Grades;

            if (filter.StudentId.HasValue)
                query = query.Where(g => g.StudentId == filter.StudentId.Value);
            if (filter.DisciplineId.HasValue)
                query = query.Where(g => g.DisciplineId == filter.DisciplineId.Value);
            if (filter.Value.HasValue)
                query = query.Where(g => g.Value == filter.Value.Value);

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Grade> AddGradeAsync(Grade grade, CancellationToken cancellationToken)
        {
            _dbContext.Grades.Add(grade);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return grade;
        }

        public async Task<Grade?> UpdateGradeAsync(int id, Grade grade, CancellationToken cancellationToken)
        {
            var existing = await _dbContext.Grades.FindAsync(id);
            if (existing == null) return null;

            existing.Value = grade.Value;
            existing.StudentId = grade.StudentId;
            existing.DisciplineId = grade.DisciplineId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return existing;
        }
    }
}
