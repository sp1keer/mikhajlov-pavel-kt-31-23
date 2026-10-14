using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikhajlov_pavel_kt_31_23.Database.Helpers;
using mikhajlov_pavel_kt_31_23.Models;

namespace mikhajlov_pavel_kt_31_23.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "Groups";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(p => p.GroupId).HasName($"pk_{TableName}_group_id");
            builder.Property(p => p.GroupId).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).HasColumnName("name").HasColumnType(ColumnType.String).HasMaxLength(50);
            builder.Property(p => p.Course).HasColumnName("course");
            builder.Property(p => p.IsDeleted).HasColumnName("is_deleted");

            // Связь со Specialtys (Один ко многим)
            builder.HasOne(p => p.Specialty)
                   .WithMany(s => s.Groups)
                   .HasForeignKey(p => p.SpecialtyId)
                   .HasConstraintName($"fk_{TableName}_specialty_id")
                   .OnDelete(DeleteBehavior.Cascade); // Если удалили специальность, удалятся и группы

            builder.HasIndex(p => p.SpecialtyId, $"idx_{TableName}_specialty_id");

            // Подгрузка данных
            builder.Navigation(p => p.Specialty).AutoInclude();
        }
    }
}
