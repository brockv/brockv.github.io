-- Buyers table
CREATE TABLE [dbo].[Buyers]
(
    [FullName]          NVARCHAR (30)		 NOT NULL,

    CONSTRAINT [PK_dbo.Buyers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[Buyers] (FullName) VALUES
('Jane Stone'),
('Tom McMasters'),
('Otto Vanderwall');
GO

-- Sellers table
CREATE TABLE [dbo].[Sellers]
(
    [FullName]          NVARCHAR (30)		 NOT NULL,

    CONSTRAINT [PK_dbo.Buyers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[Sellers] (FullName) VALUES
('Gayle Hardy'),
('Lyle Banks'),
('Pearl Greene');
GO