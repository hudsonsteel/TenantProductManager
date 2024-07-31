using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenantProductManager.Domain.Entities;

namespace TenantProductManager.Infrastructure.DataBaseContext
{
    public class UserConfiguration() : BaseConfiguration<User>()
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .IsRequired();

            builder.Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.HasIndex(u => u.Email)
            .IsUnique();

            builder.HasOne(u => u.Tenant)
               .WithMany(t => t.Users)
               .HasForeignKey(u => u.TenantId)
               .OnDelete(DeleteBehavior.SetNull);

            base.Configure(builder);
        }
    }
}
