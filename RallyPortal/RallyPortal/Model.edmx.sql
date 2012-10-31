
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/27/2012 19:56:59
-- Generated from EDMX file: C:\Users\Vialpando\Documents\GitHub\RallyPortal\RallyPortal\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RallyPortal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ArticleComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentSet] DROP CONSTRAINT [FK_ArticleComment];
GO
IF OBJECT_ID(N'[dbo].[FK_GalleryImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ImageSet] DROP CONSTRAINT [FK_GalleryImage];
GO
IF OBJECT_ID(N'[dbo].[FK_SubGalleryCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GalleryCategorySet] DROP CONSTRAINT [FK_SubGalleryCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_GalleryCategoryGallery]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GallerySet] DROP CONSTRAINT [FK_GalleryCategoryGallery];
GO
IF OBJECT_ID(N'[dbo].[FK_Highlights_inherits_Article]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArticleSet_Highlights] DROP CONSTRAINT [FK_Highlights_inherits_Article];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ArticleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArticleSet];
GO
IF OBJECT_ID(N'[dbo].[CommentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentSet];
GO
IF OBJECT_ID(N'[dbo].[TeamSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeamSet];
GO
IF OBJECT_ID(N'[dbo].[ImageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ImageSet];
GO
IF OBJECT_ID(N'[dbo].[GallerySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GallerySet];
GO
IF OBJECT_ID(N'[dbo].[GalleryCategorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GalleryCategorySet];
GO
IF OBJECT_ID(N'[dbo].[ArticleSet_Highlights]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArticleSet_Highlights];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ArticleSet'
CREATE TABLE [dbo].[ArticleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [PublishedDate] datetime  NOT NULL,
    [LastModifiedDate] datetime  NOT NULL,
    [Published] bit  NOT NULL,
    [ImageUrl] nvarchar(max)  NOT NULL,
    [AuthorName] nvarchar(max)  NOT NULL,
    [AuthorEmail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CommentSet'
CREATE TABLE [dbo].[CommentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [PostDate] datetime  NOT NULL,
    [AuthorName] nvarchar(max)  NOT NULL,
    [AuthorEmail] nvarchar(max)  NOT NULL,
    [Article_Id] int  NOT NULL
);
GO

-- Creating table 'TeamSet'
CREATE TABLE [dbo].[TeamSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TeamName] nvarchar(max)  NOT NULL,
    [DriverName] nvarchar(max)  NOT NULL,
    [CoDriverName] nvarchar(max)  NOT NULL,
    [TeamImageUrl] nvarchar(max)  NOT NULL,
    [DriverImageUrl] nvarchar(max)  NOT NULL,
    [CoDriverImageUrl] nvarchar(max)  NOT NULL,
    [TeamSummary] nvarchar(max)  NOT NULL,
    [DriverSummary] nvarchar(max)  NOT NULL,
    [CoDriverSummary] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ImageSet'
CREATE TABLE [dbo].[ImageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [GalleryId] int  NOT NULL,
    [ImageId] int  NOT NULL
);
GO

-- Creating table 'GallerySet'
CREATE TABLE [dbo].[GallerySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Published] bit  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [GalleryCategoryId] int  NOT NULL
);
GO

-- Creating table 'GalleryCategorySet'
CREATE TABLE [dbo].[GalleryCategorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [GalleryCategoryId] int  NOT NULL,
    [GalleryCategoryId1] int  NULL
);
GO

-- Creating table 'ArticleSet_Highlights'
CREATE TABLE [dbo].[ArticleSet_Highlights] (
    [VideoUrl] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ArticleSet'
ALTER TABLE [dbo].[ArticleSet]
ADD CONSTRAINT [PK_ArticleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [PK_CommentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TeamSet'
ALTER TABLE [dbo].[TeamSet]
ADD CONSTRAINT [PK_TeamSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [PK_ImageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GallerySet'
ALTER TABLE [dbo].[GallerySet]
ADD CONSTRAINT [PK_GallerySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GalleryCategorySet'
ALTER TABLE [dbo].[GalleryCategorySet]
ADD CONSTRAINT [PK_GalleryCategorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArticleSet_Highlights'
ALTER TABLE [dbo].[ArticleSet_Highlights]
ADD CONSTRAINT [PK_ArticleSet_Highlights]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Article_Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_ArticleComment]
    FOREIGN KEY ([Article_Id])
    REFERENCES [dbo].[ArticleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleComment'
CREATE INDEX [IX_FK_ArticleComment]
ON [dbo].[CommentSet]
    ([Article_Id]);
GO

-- Creating foreign key on [GalleryId] in table 'ImageSet'
ALTER TABLE [dbo].[ImageSet]
ADD CONSTRAINT [FK_GalleryImage]
    FOREIGN KEY ([GalleryId])
    REFERENCES [dbo].[GallerySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GalleryImage'
CREATE INDEX [IX_FK_GalleryImage]
ON [dbo].[ImageSet]
    ([GalleryId]);
GO

-- Creating foreign key on [GalleryCategoryId] in table 'GallerySet'
ALTER TABLE [dbo].[GallerySet]
ADD CONSTRAINT [FK_GalleryCategoryGallery]
    FOREIGN KEY ([GalleryCategoryId])
    REFERENCES [dbo].[GalleryCategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GalleryCategoryGallery'
CREATE INDEX [IX_FK_GalleryCategoryGallery]
ON [dbo].[GallerySet]
    ([GalleryCategoryId]);
GO

-- Creating foreign key on [GalleryCategoryId1] in table 'GalleryCategorySet'
ALTER TABLE [dbo].[GalleryCategorySet]
ADD CONSTRAINT [FK_GalleryCategoryGalleryCategory]
    FOREIGN KEY ([GalleryCategoryId1])
    REFERENCES [dbo].[GalleryCategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GalleryCategoryGalleryCategory'
CREATE INDEX [IX_FK_GalleryCategoryGalleryCategory]
ON [dbo].[GalleryCategorySet]
    ([GalleryCategoryId1]);
GO

-- Creating foreign key on [Id] in table 'ArticleSet_Highlights'
ALTER TABLE [dbo].[ArticleSet_Highlights]
ADD CONSTRAINT [FK_Highlights_inherits_Article]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ArticleSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------