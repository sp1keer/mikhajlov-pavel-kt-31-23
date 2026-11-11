using Microsoft.EntityFrameworkCore;
using mikhajlov_pavel_kt_31_23.Database;

namespace mikhajlov_pavel_kt_31_23.Services.Interfaces
{
    public interface IStudentDisciplineService
    {
        Task<string[]> GetDisciplinesByFirstNameAsync(string firstName, CancellationToken cancellationToken);
    }

    public class StudentDisciplineService : IStudentDisciplineService
    {
        private readonly StudentDbContext _dbContext;
        public StudentDisciplineService(StudentDbContext dbContext) => _dbContext = dbContext;

        public async Task<string[]> GetDisciplinesByFirstNameAsync(string firstName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Array.Empty<string>();
            }

            var cleanName = firstName.Trim().ToLower();

            // 1. Находим ID всех активных студентов по имени (FirstName)
            var studentIds = await _dbContext.Students
                .AsNoTracking()
                .Where(s => !s.IsDeleted)
                .Where(s => s.FirstName.ToLower() == cleanName 
                         || s.FirstName.ToLower().Contains(cleanName))
                .Select(s => s.StudentId)
                .ToListAsync(cancellationToken);

            if (studentIds.Count == 0)
            {
                return Array.Empty<string>();
            }

            // 2. Находим только те дисциплины, по которым у этих студентов есть оценки
            var disciplines = await _dbContext.Grades
                .AsNoTracking()
                .Where(g => studentIds.Contains(g.StudentId))
                .Where(g => g.Discipline != null && !g.Discipline.IsDeleted)
                .Select(g => g.Discipline!.Name)
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct()
                .ToArrayAsync(cancellationToken);

            return disciplines;
        }
    }
}
