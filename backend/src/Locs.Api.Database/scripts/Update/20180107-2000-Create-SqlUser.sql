-- This script creates the LocsUser on the server and in the database if they don't already exist.
-- This allows the database to be automatically created from scratch with no manual intervention
IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'LocsUser')
BEGIN
	CREATE LOGIN [LocsUser] WITH PASSWORD='takecare*99', DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
END
GO

IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = 'LocsUser')
BEGIN
	CREATE USER [LocsUser] FOR LOGIN [LocsUser] WITH DEFAULT_SCHEMA = [dbo]
	EXEC sp_addrolemember N'db_datareader', N'LocsUser'		
	EXEC sp_addrolemember N'db_datawriter', N'LocsUser'
END
GO

-- Create schema
IF NOT EXISTS (
SELECT  schema_name
FROM    information_schema.schemata
WHERE   schema_name = 'Locs' ) 

BEGIN
EXEC sp_executesql N'CREATE SCHEMA Locs'
END