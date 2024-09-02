IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'mtfu.development')
BEGIN
    CREATE DATABASE [mtfu.development];
END;
GO

-- Conceder permissões ao usuário 'sa'
USE [mtfu.development];
GO
ALTER AUTHORIZATION ON DATABASE::[mtfu.development] TO sa;
GO