CREATE PROCEDURE [dbo].[spGetFilteredOrders]
	@Year INTEGER = NULL,
	@Month INTEGER = NULL,
	@Status nvarchar(20) = NULL,
	@Product INTEGER = NULL
AS
	BEGIN
		SET NOCOUNT ON;

		SELECT * FROM Orders
		WHERE (@Year IS NULL OR YEAR(Orders.createdDate) = @Year)
		AND (@Month IS NULL OR Month(Orders.createdDate) = @Month)
		AND (@Status IS NULL OR Orders.status = @Status)
		AND (@Product IS NULL OR Orders.productId = @Product)
	END
GO
