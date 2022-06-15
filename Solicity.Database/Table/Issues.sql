﻿CREATE TABLE [dbo].[Issues]
(
    -- Base Entity
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UpdatedAt] DATETIME NOT NULL, 
    [UpdatedBy] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedAt] DATETIME NOT NULL, 
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL, 

    -- Issue data
    [TopicId] UNIQUEIDENTIFIER NOT NULL,
    [Code] VARCHAR(50) NOT NULL UNIQUE,
    [IsClosed] BIT NOT NULL, 
    [Title] VARCHAR(100) NOT NULL,
)
