CREATE PROCEDURE [dbo].[sqlUserLookup]
	@Id nvarchar(128) = 0
AS
begin
	set nocount on;

	SELECT Id,  FirstName, LastName, EmailAddress, CreateDate
	from [dbo].[User]
	where Id = @Id
end

