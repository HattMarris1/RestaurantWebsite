CREATE TABLE [dbo].[MenuItem]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] Varchar(20) NOT NULL, 
    [IsVeg] BIT NULL DEFAULT 0, 
    [IsNew] BIT NULL DEFAULT 0, 
    [Description] VARCHAR(50) NULL, 
    [Category] INT NULL,

)
