IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.COLUMNS
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'FoodStock'
				 AND COLUMN_NAME = 'ProviderId'))
BEGIN
	TRUNCATE TABLE dbo.FoodStock
    
	ALTER TABLE dbo.FoodStock 	
	ADD ProviderId int NOT NULL

	CREATE TABLE dbo.Providers 
	(
		Id int NOT NULL IDENTITY(1,1),
		Name NVARCHAR(200) NOT NULL,
		PRIMARY KEY (Id)
	)

	ALTER TABLE dbo.FoodStock 
	ADD CONSTRAINT FK_FoodStock_Providers
	FOREIGN KEY (ProviderId)
	REFERENCES Providers(Id)

	EXEC sp_executesql N'INSERT Providers (Name) VALUES (''Tesco Riverhead''), (''Tesco Sevenoaks''), (''Sainsbury Sevenoaks''), (''Waitrose Sevenoas'')'
END