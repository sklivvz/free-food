IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'PayPalTransactions'))
BEGIN
    CREATE TABLE dbo.PayPalTransactions
	(
	Id int NOT NULL IDENTITY (1, 1),
	Description nvarchar(200) NOT NULL,
	Amount numeric(15,2) NOT NULL,
	[Date] datetime NOT NULL,
	)  ON [PRIMARY]
	ALTER TABLE dbo.PayPalTransactions ADD CONSTRAINT
	PK_PayPalTransactions PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

END