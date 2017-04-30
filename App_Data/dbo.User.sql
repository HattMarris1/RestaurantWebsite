CREATE TABLE [dbo].[User] (
    [Id]       INT          NOT NULL,
    [UserName] VARCHAR (20) NULL,
    [Password] VARCHAR (20) NULL,
    [IsAdmin]  BIT          NULL,
    [FavFood]  VARCHAR (15) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

