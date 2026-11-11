using Microsoft.EntityFrameworkCore;
using mikhajlov_pavel_kt_31_23.Filters;
using mikhajlov_pavel_kt_31_23.Models;
using mikhajlov_pavel_kt_31_23.Database;

namespace mikhajlov_pavel_kt_31_23.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Student[]> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken);
        Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken);
        Task<Student?> UpdateStudentAsync(int id, Student student, CancellationToken cancellationToken);
        Task DeleteStudentAsync(int id, CancellationToken cancellationToken);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;
        public StudentService(StudentDbContext dbContext) => _dbContext = dbContext;

        public async Task<Student[]> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken)
        {
            IQueryable<Student> query = _dbContext.Students.Include(s => s.Group);

            if (filter.GroupId.HasValue) query = query.Where(s => s.GroupId == filter.GroupId.Value);
            if (!string.IsNullOrWhiteSpace(filter.FirstName)) query = query.Where(s => s.FirstName.Contains(filter.FirstName));
            if (!string.IsNullOrWhiteSpace(filter.LastName)) query = query.Where(s => s.LastName.Contains(filter.LastName));
            if (filter.IsDeleted.HasValue) query = query.Where(s => s.IsDeleted == filter.IsDeleted.Value);

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken)
        {
            student.IsDeleted = false;
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return student;
        }

        public async Task<Student?> UpdateStudentAsync(int id, Student student, CancellationToken cancellationToken)
        {
            var existing = await _dbContext.Students.FindAsync(id);
            if (existing == null) return null;

            existing.FirstName = student.FirstName;
            existing.LastName = student.LastName;
            existing.GroupId = student.GroupId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task DeleteStudentAsync(int id, CancellationToken cancellationToken)
        {
            var student = await _dbContext.Students.FindAsync(id);
            if (student == null) return;

            student.IsDeleted = true; // Soft delete студента
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
