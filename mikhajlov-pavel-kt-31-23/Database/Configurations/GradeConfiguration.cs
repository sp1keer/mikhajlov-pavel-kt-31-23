using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikhajlov_pavel_kt_31_23.Models;

namespace mikhajlov_pavel_kt_31_23.Database.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        private const string TableName = "Grades";

        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(p => p.GradeId).HasName($"pk_{TableName}_grade_id");
            builder.Property(p => p.GradeId).ValueGeneratedOnAdd();

            builder.Property(p => p.Value).HasColumnName("value");

            // Связь со Students
            builder.HasOne(p => p.Student)
                   .WithMany(s => s.Grades)
                   .HasForeignKey(p => p.StudentId)
                   .HasConstraintName($"fk_{TableName}_student_id")
                   .OnDelete(DeleteBehavior.Cascade);

            // Связь с Disciplines
            builder.HasOne(p => p.Discipline)
                   .WithMany(d => d.Grades)
                   .HasForeignKey(p => p.DisciplineId)
                   .HasConstraintName($"fk_{TableName}_discipline_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.StudentId, $"idx_{TableName}_student_id");
            builder.HasIndex(p => p.DisciplineId, $"idx_{TableName}_discipline_id");

            builder.Navigation(p => p.Student).AutoInclude();
            builder.Navigation(p => p.Discipline).AutoInclude();
        }
    }
}
