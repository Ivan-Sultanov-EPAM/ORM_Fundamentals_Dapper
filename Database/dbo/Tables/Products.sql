CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[name] NVARCHAR(20) NULL, 
    [description] NVARCHAR(50) NULL, 
    [weight] DECIMAL(5, 2) NULL,
    [height] DECIMAL(5, 2) NULL,
    [width] DECIMAL(5, 2) NULL,
    [length] DECIMAL(5, 2) NULL
)
