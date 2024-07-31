using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenantProductManager.Domain.Interfaces.Entities;
using TenantProductManager.Domain.Interfaces.Services;

namespace TenantProductManager.Infrastructure.DataBaseContext
{
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
    {
        private readonly ITenantProvider _tenantProvider;

        protected BaseConfiguration(ITenantProvider tenantProvider)
        {
            _tenantProvider = tenantProvider;
        }

        protected BaseConfiguration()
        {

        }
        protected int _tenantId => _tenantProvider.GetTenantId();


        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(c => c.Id);

            SetAuditTables(builder);
        }

        protected void SetAuditTables(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property("CreatedAt")
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property("UpdatedAt")
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
