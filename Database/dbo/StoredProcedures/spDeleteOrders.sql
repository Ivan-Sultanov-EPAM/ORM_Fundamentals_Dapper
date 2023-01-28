CREATE PROCEDURE [dbo].[spDeleteOrders]
	@Year INTEGER = NULL,
	@Month INTEGER = NULL,
	@Status nvarchar(20) = NULL,
	@Product INTEGER = NULL
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRANSACTION
		BEGIN TRY

			DELETE FROM Orders
			WHERE (@Year IS NULL OR YEAR(Orders.created_date) = @Year)
			AND (@Month IS NULL OR Month(Orders.created_date) = @Month)
			AND (@Status IS NULL OR Orders.status = @Status)
			AND (@Product IS NULL OR Orders.product_id = @Product)

			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
			PRINT 'Error occurred during delete operation.'
		END CATCH
END
GO
