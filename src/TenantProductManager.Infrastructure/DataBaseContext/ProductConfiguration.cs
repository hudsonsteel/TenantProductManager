using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Services;

namespace TenantProductManager.Infrastructure.DataBaseContext
{
    public class ProductConfiguration(ITenantProvider tenantProvider) : BaseConfiguration<Product>(tenantProvider)
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Tenant)
           .WithMany(t => t.Products)
           .HasForeignKey(p => p.TenantId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => x.TenantId == TenantId);

            base.Configure(builder);
        }
    }
}
