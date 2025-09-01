using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorageProject.Domain.Entity;

namespace StorageProject.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x=> x.Profile).IsRequired();

        }
    }
}
