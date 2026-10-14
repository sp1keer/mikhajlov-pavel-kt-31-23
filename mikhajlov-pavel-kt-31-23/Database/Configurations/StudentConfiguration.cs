using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikhajlov_pavel_kt_31_23.Database.Helpers;
using mikhajlov_pavel_kt_31_23.Models;

namespace mikhajlov_pavel_kt_31_23.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        private const string TableName = "Students";

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(p => p.StudentId).HasName($"pk_{TableName}_student_id");
            builder.Property(p => p.StudentId).ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName).HasColumnName("first_name").HasColumnType(ColumnType.String).HasMaxLength(100);
            builder.Property(p => p.LastName).HasColumnName("last_name").HasColumnType(ColumnType.String).HasMaxLength(100);
            builder.Property(p => p.IsDeleted).HasColumnName("is_deleted");

            // Связь с Groups
            builder.HasOne(p => p.Group)
                   .WithMany(g => g.Students)
                   .HasForeignKey(p => p.GroupId)
                   .HasConstraintName($"fk_{TableName}_group_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.GroupId, $"idx_{TableName}_group_id");
            builder.Navigation(p => p.Group).AutoInclude();
        }
    }
}
