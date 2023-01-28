CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [status] NVARCHAR(20) NULL, 
    [created_date] DATE NULL, 
    [updated_date] DATE NULL, 
    [product_id] INT NOT NULL,
    CONSTRAINT [FK_Orders_Products] FOREIGN KEY ([product_id]) REFERENCES [Products]([Id])
    ON DELETE CASCADE
)
