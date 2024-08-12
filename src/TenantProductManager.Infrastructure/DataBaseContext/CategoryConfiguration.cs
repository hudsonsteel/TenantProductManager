using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Services;

namespace TenantProductManager.Infrastructure.DataBaseContext
{
    public sealed class CategoryConfiguration(ITenantProvider tenantProvider) : BaseConfiguration<Category>(tenantProvider)
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.ParentCategoryId)
                .IsRequired(false);

            builder.HasOne(c => c.Tenant)
                .WithMany(t => t.Categories)
                .HasForeignKey(c => c.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => x.TenantId == TenantId);


            base.Configure(builder);
        }
    }
}
