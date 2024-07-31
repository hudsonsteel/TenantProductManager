using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Services;

namespace TenantProductManager.Infrastructure.DataBaseContext
{
    public class TenantConfiguration(ITenantProvider tenantProvider) : BaseConfiguration<Tenant>(tenantProvider)
    {
        public override void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(t => t.IsRoot)
                   .IsRequired();

            builder.Property(t => t.RootTenantId)
                   .IsRequired(false);

            builder.HasOne(t => t.ParentTenant)
                   .WithMany(t => t.Children)
                   .HasForeignKey(t => t.ParentTenantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.RootTenant)
                   .WithMany()
                   .HasForeignKey(t => t.RootTenantId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasIndex(t => t.ParentTenantId);
            builder.HasIndex(t => t.RootTenantId);

            base.Configure(builder);
        }
    }
}
