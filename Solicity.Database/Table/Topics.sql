CREATE TABLE [dbo].[Topics]
(
    -- Base Entity
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UpdatedAt] DATETIME NOT NULL, 
    [UpdatedBy] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedAt] DATETIME NOT NULL, 
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL, 

    -- Topic data
    [Name] VARCHAR(64) NOT NULL, 
    [Description] VARCHAR(256) NOT NULL,
    [Code] VARCHAR(4) NOT NULL UNIQUE, 
    [Enabled] BIT NOT NULL,
)
