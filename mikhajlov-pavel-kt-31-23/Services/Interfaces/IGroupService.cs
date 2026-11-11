using Microsoft.EntityFrameworkCore;
using mikhajlov_pavel_kt_31_23.Filters;
using mikhajlov_pavel_kt_31_23.Database;
using mikhajlov_pavel_kt_31_23.Models;

namespace mikhajlov_pavel_kt_31_23.Services.Interfaces
{
    public interface IGroupService
    {
        Task<Group[]> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken);
        Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken);
        Task<Group?> UpdateGroupAsync(int id, Group group, CancellationToken cancellationToken);
        Task DeleteGroupAsync(int id, CancellationToken cancellationToken);
    }

    public class GroupService : IGroupService
    {
        private readonly StudentDbContext _dbContext;
        public GroupService(StudentDbContext dbContext) => _dbContext = dbContext;

        public async Task<Group[]> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken)
        {
            IQueryable<Group> query = _dbContext.Groups.Include(g => g.Specialty);

            if (filter.SpecialtyId.HasValue) query = query.Where(g => g.SpecialtyId == filter.SpecialtyId.Value);
            if (filter.Course.HasValue) query = query.Where(g => g.Course == filter.Course.Value);
            if (filter.IsDeleted.HasValue) query = query.Where(g => g.IsDeleted == filter.IsDeleted.Value);

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken)
        {
            group.IsDeleted = false;
            _dbContext.Groups.Add(group);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }

        public async Task<Group?> UpdateGroupAsync(int id, Group group, CancellationToken cancellationToken)
        {
            var existing = await _dbContext.Groups.FindAsync(id);
            if (existing == null) return null;

            existing.Name = group.Name;
            existing.Course = group.Course;
            existing.SpecialtyId = group.SpecialtyId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task DeleteGroupAsync(int id, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(id);
            if (group == null) return;

            group.IsDeleted = true; // Soft delete группы

            // Soft delete всех студентов этой группы
            var students = await _dbContext.Students.Where(s => s.GroupId == id).ToListAsync(cancellationToken);
            foreach (var student in students)
            {
                student.IsDeleted = true;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
