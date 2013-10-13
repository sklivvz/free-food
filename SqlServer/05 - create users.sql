IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Users'))
BEGIN
    CREATE TABLE dbo.Users
	(
	Id int NOT NULL IDENTITY (1, 1),
	Name nvarchar(200) NOT NULL
	)  ON [PRIMARY]
	ALTER TABLE dbo.Users ADD CONSTRAINT
	PK_Users PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	EXEC sp_executesql N'insert into users (name) values (N''T Bone Slim''), (N''Utah Phillips''), (N''Alexander Supertramp''), (N''William Shatner'')'

END