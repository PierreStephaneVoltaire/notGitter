
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/02/2018 09:53:05
-- Generated from EDMX file: C:\Users\steph\Source\Repos\notgitter\notgitter\ChatroomModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ChatRoomDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Chatrooms'
CREATE TABLE [dbo].[Chatrooms] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Repoes'
CREATE TABLE [dbo].[Repoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [userId] int  NOT NULL,
    [dateCreated] datetime  NOT NULL,
    [language] nvarchar(max)  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [url] nvarchar(max)  NOT NULL,
    [private] bit  NOT NULL,
    [Chatroom_Id] int  NOT NULL
);
GO

-- Creating table 'Users1'
CREATE TABLE [dbo].[Users1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [email] nvarchar(max)  NULL,
    [GithubId] int  NOT NULL,
    [online] bit  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [content] nvarchar(max)  NOT NULL,
    [timestamp] datetime  NOT NULL,
    [userId] int  NOT NULL,
    [ChatroomMessage_Message_Id] int  NOT NULL
);
GO

-- Creating table 'collab'
CREATE TABLE [dbo].[collab] (
    [collaborators_Id] int  NOT NULL,
    [collab_user_Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'Repoes'
ALTER TABLE [dbo].[Repoes]
ADD CONSTRAINT [PK_Repoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users1'
ALTER TABLE [dbo].[Users1]
ADD CONSTRAINT [PK_Users1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [collaborators_Id], [collab_user_Id] in table 'collab'
ALTER TABLE [dbo].[collab]
ADD CONSTRAINT [PK_collab]
    PRIMARY KEY CLUSTERED ([collaborators_Id], [collab_user_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [userId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_senderId]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[Users1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_senderId'
CREATE INDEX [IX_FK_senderId]
ON [dbo].[Messages]
    ([userId]);
GO

-- Creating foreign key on [ChatroomMessage_Message_Id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_ChatroomMessage]
    FOREIGN KEY ([ChatroomMessage_Message_Id])
    REFERENCES [dbo].[Chatrooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatroomMessage'
CREATE INDEX [IX_FK_ChatroomMessage]
ON [dbo].[Messages]
    ([ChatroomMessage_Message_Id]);
GO

-- Creating foreign key on [userId] in table 'Repoes'
ALTER TABLE [dbo].[Repoes]
ADD CONSTRAINT [FK_userRepo]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[Users1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_userRepo'
CREATE INDEX [IX_FK_userRepo]
ON [dbo].[Repoes]
    ([userId]);
GO

-- Creating foreign key on [collaborators_Id] in table 'collab'
ALTER TABLE [dbo].[collab]
ADD CONSTRAINT [FK_collab_user]
    FOREIGN KEY ([collaborators_Id])
    REFERENCES [dbo].[Users1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [collab_user_Id] in table 'collab'
ALTER TABLE [dbo].[collab]
ADD CONSTRAINT [FK_collab_Repo]
    FOREIGN KEY ([collab_user_Id])
    REFERENCES [dbo].[Repoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_collab_Repo'
CREATE INDEX [IX_FK_collab_Repo]
ON [dbo].[collab]
    ([collab_user_Id]);
GO

-- Creating foreign key on [Chatroom_Id] in table 'Repoes'
ALTER TABLE [dbo].[Repoes]
ADD CONSTRAINT [FK_has]
    FOREIGN KEY ([Chatroom_Id])
    REFERENCES [dbo].[Chatrooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_has'
CREATE INDEX [IX_FK_has]
ON [dbo].[Repoes]
    ([Chatroom_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------