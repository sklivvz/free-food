IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.COLUMNS
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'FoodStock'
				 AND COLUMN_NAME = 'FoodGroup'))
BEGIN
	TRUNCATE TABLE dbo.FoodStock
    ALTER TABLE dbo.FoodStock 	ADD FoodGroup int NOT NULL
END