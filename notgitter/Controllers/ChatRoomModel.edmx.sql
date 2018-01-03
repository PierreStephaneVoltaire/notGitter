
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/03/2018 14:51:43
-- Generated from EDMX file: C:\Users\xudaw\Source\Repos\notGitter2\notgitter\Controllers\ChatRoomModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ChatRoomDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_owns]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Repoes] DROP CONSTRAINT [FK_owns];
GO
IF OBJECT_ID(N'[dbo].[FK_collab_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[collab] DROP CONSTRAINT [FK_collab_User];
GO
IF OBJECT_ID(N'[dbo].[FK_collab_Repo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[collab] DROP CONSTRAINT [FK_collab_Repo];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatroomMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_ChatroomMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatroomRepo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chatrooms] DROP CONSTRAINT [FK_ChatroomRepo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserMessage];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Chatrooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chatrooms];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[Repoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Repoes];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[collab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[collab];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Chatrooms'
CREATE TABLE [dbo].[Chatrooms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ChatroomRepo_Chatroom_Id] int  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [content] nchar(10)  NULL,
    [timestamp] binary(8)  NULL,
    [ChatroomId] int  NOT NULL
);
GO

-- Creating table 'Repoes'
CREATE TABLE [dbo].[Repoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [dateCreated] datetime  NULL,
    [language] varchar(50)  NULL,
    [name] varchar(50)  NULL,
    [url] varchar(50)  NULL,
    [C_private_] binary(50)  NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NULL,
    [email] varchar(50)  NULL,
    [GithubId] int  NULL,
    [online] binary(50)  NULL,
    [UserMessage_User_Id] int  NOT NULL
);
GO

-- Creating table 'collab'
CREATE TABLE [dbo].[collab] (
    [Collaborators_Id] int  NOT NULL,
    [collab_User_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Chatrooms'
ALTER TABLE [dbo].[Chatrooms]
ADD CONSTRAINT [PK_Chatrooms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Repoes'
ALTER TABLE [dbo].[Repoes]
ADD CONSTRAINT [PK_Repoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Collaborators_Id], [collab_User_Id] in table 'collab'
ALTER TABLE [dbo].[collab]
ADD CONSTRAINT [PK_collab]
    PRIMARY KEY CLUSTERED ([Collaborators_Id], [collab_User_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Repoes'
ALTER TABLE [dbo].[Repoes]
ADD CONSTRAINT [FK_owns]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_owns'
CREATE INDEX [IX_FK_owns]
ON [dbo].[Repoes]
    ([UserId]);
GO

-- Creating foreign key on [Collaborators_Id] in table 'collab'
ALTER TABLE [dbo].[collab]
ADD CONSTRAINT [FK_collab_User]
    FOREIGN KEY ([Collaborators_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [collab_User_Id] in table 'collab'
ALTER TABLE [dbo].[collab]
ADD CONSTRAINT [FK_collab_Repo]
    FOREIGN KEY ([collab_User_Id])
    REFERENCES [dbo].[Repoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_collab_Repo'
CREATE INDEX [IX_FK_collab_Repo]
ON [dbo].[collab]
    ([collab_User_Id]);
GO

-- Creating foreign key on [ChatroomId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_ChatroomMessage]
    FOREIGN KEY ([ChatroomId])
    REFERENCES [dbo].[Chatrooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatroomMessage'
CREATE INDEX [IX_FK_ChatroomMessage]
ON [dbo].[Messages]
    ([ChatroomId]);
GO

-- Creating foreign key on [ChatroomRepo_Chatroom_Id] in table 'Chatrooms'
ALTER TABLE [dbo].[Chatrooms]
ADD CONSTRAINT [FK_ChatroomRepo]
    FOREIGN KEY ([ChatroomRepo_Chatroom_Id])
    REFERENCES [dbo].[Repoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatroomRepo'
CREATE INDEX [IX_FK_ChatroomRepo]
ON [dbo].[Chatrooms]
    ([ChatroomRepo_Chatroom_Id]);
GO

-- Creating foreign key on [UserMessage_User_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UserMessage]
    FOREIGN KEY ([UserMessage_User_Id])
    REFERENCES [dbo].[Messages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMessage'
CREATE INDEX [IX_FK_UserMessage]
ON [dbo].[Users]
    ([UserMessage_User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------