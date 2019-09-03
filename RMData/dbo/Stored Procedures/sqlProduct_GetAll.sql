CREATE PROCEDURE [dbo].[sqlProduct_GetAll]

AS

begin
	set nocount on;


	select Id, ProductName, Descripton, RetailPrice, QuantityInStock
	from dbo.Product
	order by ProductName;
end
