CREATE TABLE [dbo].[Category]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(20) NULL, 
    [Password] VARCHAR(20) NULL, 
    [IsAdmin] BIT NULL, 
    [FavFood] VARCHAR(15) NULL
)
