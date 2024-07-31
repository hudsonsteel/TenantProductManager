
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TenantProductManager')
BEGIN
    CREATE DATABASE TenantProductManager;
END
GO

USE TenantProductManager;
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'TenantProductManagerUser')
BEGIN
    CREATE LOGIN TenantProductManagerUser WITH PASSWORD = 'TenantProductManager&Password1';
    CREATE USER TenantProductManagerUser FOR LOGIN TenantProductManagerUser;
    ALTER ROLE db_owner ADD MEMBER TenantProductManagerUser;
END

-- Insert a default tenant (if not already present)
IF NOT EXISTS (SELECT 1 FROM TenantProductManager.dbo.Tenants WHERE Name = 'Default Tenant')
BEGIN
    INSERT INTO TenantProductManager.dbo.Tenants (Name, ParentTenantId, IsRoot, RootTenantId, CreatedAt, UpdatedAt)
    VALUES ('Default Tenant', NULL, 1, NULL, GETUTCDATE(), GETUTCDATE());
END

-- Retrieve the tenant ID of the 'Default Tenant'
DECLARE @TenantId INT;
SELECT @TenantId = Id FROM TenantProductManager.dbo.Tenants WHERE Name = 'Default Tenant';

-- Insert an admin user associated with the default tenant
IF NOT EXISTS (SELECT 1 FROM TenantProductManager.dbo.Users WHERE Email = 'admin@example.com')
BEGIN
    --PasswordHash = Hashed@password1
    INSERT INTO TenantProductManager.dbo.Users (Name, Email, PasswordHash, TenantId, IsAdmin, CreatedAt, UpdatedAt)
    VALUES ('AdminUser', 'admin@example.com', '$2a$11$r4vk3WssZUXCT45ZVaAm0OLAzKzDTUEAYbdrS61WznRCFjNm3n1hO', @TenantId, 1, GETUTCDATE(), GETUTCDATE());
END
