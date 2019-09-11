CREATE PROCEDURE [dbo].[sqlInventory_GetAll]
	
AS
begin
	set nocount on;

	select [Id], [ProductId], [Quantity], [PurchasePrice], [PurchaseDate]
	from dbo.Inventory
	
end
