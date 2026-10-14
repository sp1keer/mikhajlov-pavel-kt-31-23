using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using mikhajlov_pavel_kt_31_23.Database.Helpers;
using mikhajlov_pavel_kt_31_23.Models;

namespace mikhajlov_pavel_kt_31_23.Database.Configurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        private const string TableName = "Specialtys";

        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(p => p.SpecialtyId).HasName($"pk_{TableName}_specialty_id");
            builder.Property(p => p.SpecialtyId).ValueGeneratedOnAdd();

            builder.Property(p => p.Title).HasColumnName("title").HasColumnType(ColumnType.String).HasMaxLength(100);
            builder.Property(p => p.Code).HasColumnName("code").HasColumnType(ColumnType.String).HasMaxLength(50);
        }
    }
}
