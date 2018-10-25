CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(20) NOT NULL, 
    [LastName] VARCHAR(20) NOT NULL, 
    [PhoneNumber] VARCHAR(20) NOT NULL, 
    [ApartmentName] VARCHAR(50) NOT NULL, 
    [UnitNumber] INT NOT NULL, 
    [RequestDate] TIMESTAMP NOT NULL
)
