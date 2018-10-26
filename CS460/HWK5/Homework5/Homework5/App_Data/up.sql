-- Service Request Form table
CREATE TABLE [dbo].[ServiceRequestForms]
(
    [ID]                 INT IDENTITY (1, 1) NOT NULL,
    [FirstName]          NVARCHAR (30)		 NOT NULL,
    [LastName]           NVARCHAR (30)		 NOT NULL,
    [PhoneNumber]        NVARCHAR (20)		 NOT NULL,
    [ApartmentName]      NVARCHAR (64)		 NOT NULL,
    [UnitNumber]         INT				 NOT NULL,
    [RequestDescription] NVARCHAR (100)		 NOT NULL,
	[AllowEntry]         BIT	             NOT NULL,
    [RequestTimestamp]   DATETIME			 NOT NULL,
    CONSTRAINT [PK_dbo.ServiceRequestForms] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[ServiceRequestForms] (FirstName, LastName, PhoneNumber, ApartmentName, UnitNumber, RequestDescription, AllowEntry, RequestTimestamp) VALUES
	('Doug', 'Douglas', '5039630325', 'Meadow Creek', '124', 'The backdoor is stuck shut.', '1', '2018/04/26 01:54:47 PM'),
	('Ivy', 'Iverson', '5039805452', 'Shady Oaks', '262', 'Upstairs neighbors are too loud after quiet hours.', '0', '2018/07/06 08:17:36 AM'),
	('Jim', 'Johnson', '5038700225', 'Robins Lane', '320', 'The bathroom faucet wont stop leaking', '1', '2018/08/16 11:44:47 PM'),
	('Sue','Suzanne', '5039872465', 'Meadow Creek', '202', 'The shower head in the main bathroom is broken.', '0', '2018/10/03 08:22:56 AM'),
	('Mira', 'Kuzak', '5038710285', 'Robins Lane', '110', 'The heaters in the bedrooms wont turn on.', '1', '2018/10/28 07:44:27 PM')
GO