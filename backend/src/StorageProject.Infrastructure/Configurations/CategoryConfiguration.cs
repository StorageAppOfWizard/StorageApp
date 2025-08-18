using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorageProject.Domain.Entity;

namespace StorageProject.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(20).HasColumnType("VARCHAR").IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).HasColumnType("VARCHAR");

        }
    }
}
