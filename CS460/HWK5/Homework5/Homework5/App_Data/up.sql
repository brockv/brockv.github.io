-- Service Request Form table
CREATE TABLE [dbo].[ServiceRequestForms]
(
	[ID]		INT IDENTITY (1,1)		NOT NULL,
	[FirstName]	NVARCHAR(64)			NOT NULL,
	[LastName]	NVARCHAR(64)			NOT NULL,
	[PhoneNumber] NVARCHAR(14)			NOT NULL,
	[ApartmentName] NVARCHAR(64)		NOT NULL,
	[UnitNumber]	INT					NOT NULL,
	[RequestDescription] NVARCHAR(250)  NOT NULL,
	[AllowEntry] INT					NULL,
	[RequestTimestamp] DATETIME			NOT NULL,
	CONSTRAINT [PK_dbo.ServiceRequestForms] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[ServiceRequestForms] (FirstName, LastName, PhoneNumber, ApartmentName, UnitNumber, RequestDescription, AllowEntry, RequestTimestamp) VALUES
	('Jim', 'Johnson', '5039870225', 'Robins Lane', '12', 'Leaky faucets.', '1', '08/16/2018 11:44:47 PM'),
	('Sue','Suzanne', '5039872465', 'Sparrow Lane', '2', 'Broken shower head.', '0', '10/03/2018 08:22:56 AM'),
	('Mira', 'Kuzak', '5038710285', 'Robins Lane', '7', 'Heaters wont turn on.', '1', '10/28/2018 07:44:27 PM')
GO