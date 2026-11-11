using System.Globalization;
using Microsoft.EntityFrameworkCore;
using mikhajlov_pavel_kt_31_23.Database;

namespace mikhajlov_pavel_kt_31_23.Services.Interfaces
{
    public interface IGroupDisciplineService
    {
        Task<string[]> GetAverageGradesByGroupNameAsync(string groupName, CancellationToken cancellationToken);
    }

    public class GroupDisciplineService : IGroupDisciplineService
    {
        private readonly StudentDbContext _dbContext;
        public GroupDisciplineService(StudentDbContext dbContext) => _dbContext = dbContext;

        public async Task<string[]> GetAverageGradesByGroupNameAsync(string groupName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                return Array.Empty<string>();
            }

            var cleanName = groupName.Trim().ToLower();

            // 1. Находим ID всех активных групп с таким названием
            var groupIds = await _dbContext.Groups
                .AsNoTracking()
                .Where(g => !g.IsDeleted)
                .Where(g => g.Name.ToLower() == cleanName || g.Name.ToLower().Contains(cleanName))
                .Select(g => g.GroupId)
                .ToListAsync(cancellationToken);

            if (groupIds.Count == 0)
            {
                return Array.Empty<string>();
            }

            // 2. Находим ID активных студентов этих групп
            var studentIds = await _dbContext.Students
                .AsNoTracking()
                .Where(s => !s.IsDeleted && groupIds.Contains(s.GroupId))
                .Select(s => s.StudentId)
                .ToListAsync(cancellationToken);

            if (studentIds.Count == 0)
            {
                return Array.Empty<string>();
            }

            // 3. Группируем оценки студентов этой группы по дисциплинам и считаем средний балл
            var averageGrades = await _dbContext.Grades
                .AsNoTracking()
                .Where(g => studentIds.Contains(g.StudentId))
                .Where(g => g.Discipline != null && !g.Discipline.IsDeleted)
                .GroupBy(g => g.Discipline!.Name)
                .Select(group => new
                {
                    DisciplineName = group.Key,
                    Average = group.Average(g => (double)g.Value)
                })
                .OrderBy(x => x.DisciplineName)
                .ToListAsync(cancellationToken);

            // 4. Формируем список формата "Название предмета: средний балл"
            return averageGrades
                .Select(x => $"{x.DisciplineName}: {Math.Round(x.Average, 2).ToString("0.##", CultureInfo.InvariantCulture)}")
                .ToArray();
        }
    }
}
