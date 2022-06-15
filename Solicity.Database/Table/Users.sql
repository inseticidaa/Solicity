CREATE TABLE [dbo].[Users]
(
    -- Base Entity
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UpdatedAt] DATETIME NOT NULL, 
    [UpdatedBy] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedAt] DATETIME NOT NULL, 
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,

    -- User Data
    [Username] VARCHAR(50) NOT NULL UNIQUE, 
    [Email] VARCHAR(256) NOT NULL UNIQUE,
    [Hash] VARCHAR(MAX) NOT NULL, 
    [FirstName] VARCHAR(50) NULL, 
    [LastName] VARCHAR(50) NULL, 
    [ProfileImage] VARCHAR(MAX) NULL, 
    [Enabled] BIT NOT NULL
)
