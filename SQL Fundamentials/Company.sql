CREATE TABLE [dbo].[Company]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(20) NOT NULL, 
    [AddressId] INT NOT NULL, 
    CONSTRAINT [FK_Company_ToAddress] FOREIGN KEY (AddressId) REFERENCES Address(Id)
)
