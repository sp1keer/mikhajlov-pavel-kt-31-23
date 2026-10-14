using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikhajlov_pavel_kt_31_23.Database.Helpers;
using mikhajlov_pavel_kt_31_23.Models;

namespace mikhajlov_pavel_kt_31_23.Database.Configurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        private const string TableName = "Disciplines";

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(p => p.DisciplineId).HasName($"pk_{TableName}_discipline_id");
            builder.Property(p => p.DisciplineId).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).HasColumnName("name").HasColumnType(ColumnType.String).HasMaxLength(100);
            builder.Property(p => p.IsDeleted).HasColumnName("is_deleted");
        }
    }
}
