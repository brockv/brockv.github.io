-- Buyers table
CREATE TABLE [dbo].[Buyers]
(
    [ID]        INT IDENTITY (1, 1)     NOT NULL,
    [Name]      NVARCHAR(30)            NOT NULL,
    
    CONSTRAINT [PK_dbo.Buyers] PRIMARY KEY CLUSTERED (Name)
);

-- Sellers table
CREATE TABLE [dbo].[Sellers]
(
    [ID]        INT IDENTITY (1, 1)      NOT NULL,
    [Name]      NVARCHAR(30)             NOT NULL,
    
    CONSTRAINT [PK_dbo.Sellers] PRIMARY KEY CLUSTERED (Name)
);

-- Items table
CREATE TABLE [dbo].[Items]
(
    [ID]            INT IDENTITY (1001,1)       NOT NULL,
    [Name]          NVARCHAR(30)                NOT NULL,
    [Description]   NVARCHAR(100)               NOT NULL,
    [Seller]        NVARCHAR(30)                NOT NULL,
    
    CONSTRAINT [PK_dbo.Items] PRIMARY KEY CLUSTERED (ID ASC),
    CONSTRAINT [FK_dbo.Items] FOREIGN KEY (Seller) REFERENCES [dbo].[Seller] (Name)

);

-- Bids table
CREATE TABLE [dbo].[Bids]
(
    [ID]            INT IDENTITY (1,1)      NOT NULL,
    [ItemID]        INT                     NOT NULL,
    [Buyer]         NVARCHAR(30)            NOT NULL,
    [Price]         INT                     NOT NULL,
    [BidTimeStamp]  DATETIME                NOT NULL
    
    CONSTRAINT [PK_dbo.Bids] PRIMARY KEY CLUSTERED (ID ASC),
    CONSTRAINT [FK_dbo.Bids] FOREIGN KEY (ItemID) REFERENCES [dbo].[Item] (ID)

);

INSERT INTO [dbo].[Buyesr](Name) VALUES
    ('Jane Stone'),
    ('Tom McMasters'),
    ('Otto Vanderwall')

INSERT INTO [dbo].[Sellers](Name) VALUES
    ('Gayle Hardy'),
    ('Lyle Banks'),
    ('Pearl Greene')

INSERT INTO [dbo].[Items](Name, Description, Seller)VALUES
    ('Abraham Lincoln Hammer'    ,'A bench mallet fashioned from a broken rail-splitting maul in 1829 and owned by Abraham Lincoln', 'Pearl Greene'),
    ('Albert Einsteins Telescope','A brass telescope owned by Albert Einstein in Germany, circa 1927', 'Gayle Hardy'),
    ('Bob Dylan Love Poems'      ,'Five versions of an original unpublished, handwritten, love poem by Bob Dylan', 'Lyle Banks');

INSERT INTO [dbo].[Bids](ItemID, Buyer, Price, Timestamp) VALUES
    (1001, 'Otto Vanderwall', 250000,'12/04/2017 09:04:22'),
    (1003,'Jane Stone', 95000 ,'12/04/2017 08:44:03')