IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.COLUMNS
                 WHERE TABLE_SCHEMA = 'dbo' 
				 AND TABLE_NAME = 'Providers'
				 AND COLUMN_NAME = 'ImageUrl'))
BEGIN
	TRUNCATE TABLE dbo.FoodStock

	DELETE FROM dbo.Providers
	
	ALTER TABLE dbo.Providers ADD Lat float NOT NULL
	ALTER TABLE dbo.Providers ADD Lng float NOT NULL
	ALTER TABLE dbo.Providers ADD ImageUrl NVARCHAR(200) NOT NULL

	EXEC sp_executesql N'INSERT Providers (Name, Lat, Lng, ImageUrl) VALUES (''Tesco Riverhead'', 51.271679, 0.192950, ''http://ui.tescoassets.com/groceries/UIAssets/Compressed/I_635040480626718442/tLogoMain.gif''), (''Tesco Sevenoaks'', 51.271679, 0.192950, ''http://ui.tescoassets.com/groceries/UIAssets/Compressed/I_635040480626718442/tLogoMain.gif''), (''Sainsburys Sevenoaks'', 51.295734, 0.192045, ''http://www.sainsburys.co.uk/support_files/style_images/common/header/header-logo.gif''), (''Waitrose Sevenoaks'', 51.271679, 0.192950, ''http://www.lavenhamwoodlandproject.co.uk/images/Waitrose-logo.gif'')'
END