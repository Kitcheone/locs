IF NOT EXISTS (SELECT * FROM dbo.sysusers WHERE NAME = 'locs_application') CREATE ROLE locs_application AUTHORIZATION db_securityadmin
GO