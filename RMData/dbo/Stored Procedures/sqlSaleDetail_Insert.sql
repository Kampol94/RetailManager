CREATE PROCEDURE [dbo].[sqlSaleDetail_Insert]
	
	@Id int output,
	@SaleId int,
	@ProductId int,
	@Quantity int,
	@PurchasePrice money,
	@Tax money
AS
begin
	set nocount on;
	insert into dbo.SaleDetail(SaleId,ProductId,Quantity,PurchasePrice,Tax)
	values(@SaleId,@ProductId,@Quantity,@PurchasePrice,@Tax)

	select @Id = @@IDENTITY;
end
