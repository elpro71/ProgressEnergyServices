
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/27/2021 16:03:43
-- Generated from EDMX file: C:\Users\lemon\workspaces\test-energy-provider\DataAccess\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SupportDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MailAddresses_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MailAddresses] DROP CONSTRAINT [FK_MailAddresses_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Mails_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mails] DROP CONSTRAINT [FK_Mails_ToTable];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[MailAddresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MailAddresses];
GO
IF OBJECT_ID(N'[dbo].[Mails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mails];
GO
IF OBJECT_ID(N'[dbo].[Tickets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tickets];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MailAddresses'
CREATE TABLE [dbo].[MailAddresses] (
    [Id] int  NOT NULL,
    [TicketNumber] int  NOT NULL,
    [Email] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Mails'
CREATE TABLE [dbo].[Mails] (
    [Id] int  NOT NULL,
    [MailContent] nvarchar(max)  NOT NULL,
    [TicketNumber] int  NOT NULL,
    [creationTime] datetimeoffset  NULL
);
GO

-- Creating table 'Tickets'
CREATE TABLE [dbo].[Tickets] (
    [TicketNumber] int  NOT NULL,
    [Status] bit  NOT NULL,
    [Creation] datetimeoffset  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'MailAddresses'
ALTER TABLE [dbo].[MailAddresses]
ADD CONSTRAINT [PK_MailAddresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Mails'
ALTER TABLE [dbo].[Mails]
ADD CONSTRAINT [PK_Mails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TicketNumber] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [PK_Tickets]
    PRIMARY KEY CLUSTERED ([TicketNumber] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TicketNumber] in table 'MailAddresses'
ALTER TABLE [dbo].[MailAddresses]
ADD CONSTRAINT [FK_MailAddresses_ToTable]
    FOREIGN KEY ([TicketNumber])
    REFERENCES [dbo].[Tickets]
        ([TicketNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MailAddresses_ToTable'
CREATE INDEX [IX_FK_MailAddresses_ToTable]
ON [dbo].[MailAddresses]
    ([TicketNumber]);
GO

-- Creating foreign key on [TicketNumber] in table 'Mails'
ALTER TABLE [dbo].[Mails]
ADD CONSTRAINT [FK_Mails_ToTable]
    FOREIGN KEY ([TicketNumber])
    REFERENCES [dbo].[Tickets]
        ([TicketNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Mails_ToTable'
CREATE INDEX [IX_FK_Mails_ToTable]
ON [dbo].[Mails]
    ([TicketNumber]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------