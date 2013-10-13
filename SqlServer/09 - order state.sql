IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.COLUMNS
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Orders'
				 AND COLUMN_NAME = 'Processed'))
BEGIN
	ALTER TABLE dbo.Orders 	
	ADD Processed bit NOT NULL DEFAULT(0)
END