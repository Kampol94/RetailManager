CREATE PROCEDURE [dbo].[sqlProduct_GetById]

	@Id int

AS
begin
	SET NOCOUNT ON

	select Id, ProductName, Descripton, RetailPrice, QuantityInStock, IsTaxable
	from dbo.Product
	where Id = @Id;
END
