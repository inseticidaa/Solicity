CREATE TABLE [dbo].[IssuesComments]
(
    -- Base Entity
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UpdatedAt] DATETIME NOT NULL, 
    [UpdatedBy] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedAt] DATETIME NOT NULL, 
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL, 

    -- Issue data
    [IssueId] UNIQUEIDENTIFIER NOT NULL, 
    [Comment] VARCHAR(MAX) NOT NULL,
)
