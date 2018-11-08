-- Search Request log table
CREATE TABLE [dbo].[SearchRequestLogs]
(
    [ID]                 INT IDENTITY (1, 1) NOT NULL,
    [RequestTimestamp]   DATETIME			 NOT NULL,
    [RequestType]        NVARCHAR (100)		 NOT NULL,
    [ClientIP]           NVARCHAR (100)		 NOT NULL,
    [BrowserAgent]       NVARCHAR (200)		 NOT NULL,
    
    CONSTRAINT [PK_dbo.SearchRequestLogs] PRIMARY KEY CLUSTERED ([ID] ASC)
);