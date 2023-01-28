CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [status] NVARCHAR(20) NOT NULL, 
    [createdDate] DATE NOT NULL, 
    [updatedDate] DATE NOT NULL, 
    [productId] INT NOT NULL,
    CONSTRAINT [FK_Orders_Products] FOREIGN KEY ([productId]) REFERENCES [Products]([Id])
    ON DELETE CASCADE
)
