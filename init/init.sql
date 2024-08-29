IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'mtfu.development')
BEGIN
    CREATE DATABASE mtfu.development;
END;
GO