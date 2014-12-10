
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/09/2014 20:03:07
-- Generated from EDMX file: C:\Users\Vladislav\Desktop\иги\Blog_MVC\Blog\Models\BlogModels.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Blog];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Comments_Posts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_Comments_Posts];
GO
IF OBJECT_ID(N'[dbo].[FK_Comments_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_Comments_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Posts_Categories]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_Posts_Categories];
GO
IF OBJECT_ID(N'[dbo].[FK_Posts_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_Posts_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsTags_Posts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostsTags] DROP CONSTRAINT [FK_PostsTags_Posts];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsTags_Tags]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostsTags] DROP CONSTRAINT [FK_PostsTags_Tags];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[PostsTags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostsTags];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tags];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PostID] int  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [AuthorID] int  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [ShortBody] nvarchar(max)  NOT NULL,
    [CategoryID] int  NOT NULL,
    [AuthorID] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(64)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(128)  NOT NULL,
    [Lastname] varchar(128)  NOT NULL,
    [Age] varchar(128)  NOT NULL,
    [Sex] varchar(128)  NOT NULL,
    [Email] varchar(128)  NOT NULL,
    [Password] nchar(10)  NOT NULL,
    [UserGroup] int  NOT NULL
);
GO

-- Creating table 'PostsTags'
CREATE TABLE [dbo].[PostsTags] (
    [Posts_ID] int  NOT NULL,
    [Tags_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Posts_ID], [Tags_ID] in table 'PostsTags'
ALTER TABLE [dbo].[PostsTags]
ADD CONSTRAINT [PK_PostsTags]
    PRIMARY KEY NONCLUSTERED ([Posts_ID], [Tags_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryID] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_Posts_Categories]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[Categories]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Posts_Categories'
CREATE INDEX [IX_FK_Posts_Categories]
ON [dbo].[Posts]
    ([CategoryID]);
GO

-- Creating foreign key on [PostID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_Comments_Posts]
    FOREIGN KEY ([PostID])
    REFERENCES [dbo].[Posts]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Comments_Posts'
CREATE INDEX [IX_FK_Comments_Posts]
ON [dbo].[Comments]
    ([PostID]);
GO

-- Creating foreign key on [AuthorID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_Comments_Users]
    FOREIGN KEY ([AuthorID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Comments_Users'
CREATE INDEX [IX_FK_Comments_Users]
ON [dbo].[Comments]
    ([AuthorID]);
GO

-- Creating foreign key on [AuthorID] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_Posts_Users]
    FOREIGN KEY ([AuthorID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Posts_Users'
CREATE INDEX [IX_FK_Posts_Users]
ON [dbo].[Posts]
    ([AuthorID]);
GO

-- Creating foreign key on [Posts_ID] in table 'PostsTags'
ALTER TABLE [dbo].[PostsTags]
ADD CONSTRAINT [FK_PostsTags_Posts]
    FOREIGN KEY ([Posts_ID])
    REFERENCES [dbo].[Posts]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tags_ID] in table 'PostsTags'
ALTER TABLE [dbo].[PostsTags]
ADD CONSTRAINT [FK_PostsTags_Tags]
    FOREIGN KEY ([Tags_ID])
    REFERENCES [dbo].[Tags]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostsTags_Tags'
CREATE INDEX [IX_FK_PostsTags_Tags]
ON [dbo].[PostsTags]
    ([Tags_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------