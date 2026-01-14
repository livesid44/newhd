-- ============================================
-- Visit Management System - Database Setup Script
-- ============================================
-- This script creates all tables and inserts initial seed data
-- Compatible with SQL Server 2016 and later
-- ============================================

USE master;
GO

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'VisitManagementDB')
BEGIN
    CREATE DATABASE VisitManagementDB;
    PRINT 'Database VisitManagementDB created successfully.';
END
ELSE
BEGIN
    PRINT 'Database VisitManagementDB already exists.';
END
GO

USE VisitManagementDB;
GO

-- ============================================
-- DROP TABLES (if re-running script)
-- ============================================
PRINT 'Dropping existing tables if they exist...';

IF OBJECT_ID('dbo.AspNetUserTokens', 'U') IS NOT NULL DROP TABLE dbo.AspNetUserTokens;
IF OBJECT_ID('dbo.AspNetUserRoles', 'U') IS NOT NULL DROP TABLE dbo.AspNetUserRoles;
IF OBJECT_ID('dbo.AspNetUserLogins', 'U') IS NOT NULL DROP TABLE dbo.AspNetUserLogins;
IF OBJECT_ID('dbo.AspNetUserClaims', 'U') IS NOT NULL DROP TABLE dbo.AspNetUserClaims;
IF OBJECT_ID('dbo.AspNetRoleClaims', 'U') IS NOT NULL DROP TABLE dbo.AspNetRoleClaims;
IF OBJECT_ID('dbo.AspNetUsers', 'U') IS NOT NULL DROP TABLE dbo.AspNetUsers;
IF OBJECT_ID('dbo.AspNetRoles', 'U') IS NOT NULL DROP TABLE dbo.AspNetRoles;
IF OBJECT_ID('dbo.EmailTemplates', 'U') IS NOT NULL DROP TABLE dbo.EmailTemplates;
IF OBJECT_ID('dbo.SmtpSettings', 'U') IS NOT NULL DROP TABLE dbo.SmtpSettings;
IF OBJECT_ID('dbo.Visits', 'U') IS NOT NULL DROP TABLE dbo.Visits;
IF OBJECT_ID('dbo.VisitUsers', 'U') IS NOT NULL DROP TABLE dbo.VisitUsers;
IF OBJECT_ID('dbo.__EFMigrationsHistory', 'U') IS NOT NULL DROP TABLE dbo.__EFMigrationsHistory;

PRINT 'Existing tables dropped.';
GO

-- ============================================
-- CREATE TABLES
-- ============================================
PRINT 'Creating tables...';

-- VisitUsers Table
CREATE TABLE [dbo].[VisitUsers] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [FullName] NVARCHAR(200) NOT NULL,
    [Email] NVARCHAR(200) NOT NULL,
    [Role] NVARCHAR(100) NOT NULL,
    [PhoneNumber] NVARCHAR(20) NULL,
    [AuthType] INT NOT NULL DEFAULT 0,
    [LdapUserId] NVARCHAR(100) NULL,
    [CreatedDate] DATETIME2 NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [PK_VisitUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
PRINT 'VisitUsers table created.';

-- Visits Table
CREATE TABLE [dbo].[Visits] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [SerialNumber] INT NOT NULL,
    [TypeOfVisit] NVARCHAR(200) NOT NULL,
    [Vertical] NVARCHAR(200) NOT NULL,
    [SalesSpoc] NVARCHAR(200) NOT NULL,
    [AccountName] NVARCHAR(200) NOT NULL,
    [DebitingProjectId] NVARCHAR(100) NOT NULL,
    [OpportunityDetails] NVARCHAR(500) NOT NULL,
    [OpportunityType] INT NOT NULL,
    [ServiceScope] NVARCHAR(500) NOT NULL,
    [SalesStage] NVARCHAR(200) NOT NULL,
    [TcvMnUsd] DECIMAL(18,2) NOT NULL,
    [VisitStatus] INT NOT NULL,
    [VisitType] NVARCHAR(200) NOT NULL,
    [VisitDate] DATETIME2 NOT NULL,
    [IntimationDate] DATETIME2 NOT NULL,
    [Location] NVARCHAR(200) NOT NULL,
    [Site] NVARCHAR(200) NOT NULL,
    [VisitorsName] NVARCHAR(500) NOT NULL,
    [NumberOfAttendees] INT NOT NULL,
    [LevelOfVisitors] NVARCHAR(200) NOT NULL,
    [VisitDuration] NVARCHAR(100) NOT NULL,
    [Remarks] NVARCHAR(1000) NULL,
    [VisitLead] NVARCHAR(200) NOT NULL,
    [KeyMessages] NVARCHAR(1000) NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL,
    [ModifiedDate] DATETIME2 NULL,
    [CreatedBy] NVARCHAR(200) NOT NULL,
    CONSTRAINT [PK_Visits] PRIMARY KEY CLUSTERED ([Id] ASC)
);
PRINT 'Visits table created.';

-- AspNetRoles Table
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] NVARCHAR(450) NOT NULL,
    [Name] NVARCHAR(256) NULL,
    [NormalizedName] NVARCHAR(256) NULL,
    [ConcurrencyStamp] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
PRINT 'AspNetRoles table created.';

-- AspNetUsers Table
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] NVARCHAR(450) NOT NULL,
    [FullName] NVARCHAR(MAX) NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL,
    [UserName] NVARCHAR(256) NULL,
    [NormalizedUserName] NVARCHAR(256) NULL,
    [Email] NVARCHAR(256) NULL,
    [NormalizedEmail] NVARCHAR(256) NULL,
    [EmailConfirmed] BIT NOT NULL,
    [PasswordHash] NVARCHAR(MAX) NULL,
    [SecurityStamp] NVARCHAR(MAX) NULL,
    [ConcurrencyStamp] NVARCHAR(MAX) NULL,
    [PhoneNumber] NVARCHAR(MAX) NULL,
    [PhoneNumberConfirmed] BIT NOT NULL,
    [TwoFactorEnabled] BIT NOT NULL,
    [LockoutEnd] DATETIMEOFFSET NULL,
    [LockoutEnabled] BIT NOT NULL,
    [AccessFailedCount] INT NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers] ([NormalizedEmail]);
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
PRINT 'AspNetUsers table created.';

-- AspNetRoleClaims Table
CREATE TABLE [dbo].[AspNetRoleClaims] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [RoleId] NVARCHAR(450) NOT NULL,
    [ClaimType] NVARCHAR(MAX) NULL,
    [ClaimValue] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId])
        REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
);
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims] ([RoleId]);
PRINT 'AspNetRoleClaims table created.';

-- AspNetUserClaims Table
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] NVARCHAR(450) NOT NULL,
    [ClaimType] NVARCHAR(MAX) NULL,
    [ClaimValue] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims] ([UserId]);
PRINT 'AspNetUserClaims table created.';

-- AspNetUserLogins Table
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR(450) NOT NULL,
    [ProviderKey] NVARCHAR(450) NOT NULL,
    [ProviderDisplayName] NVARCHAR(MAX) NULL,
    [UserId] NVARCHAR(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins] ([UserId]);
PRINT 'AspNetUserLogins table created.';

-- AspNetUserRoles Table
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR(450) NOT NULL,
    [RoleId] NVARCHAR(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId])
        REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles] ([RoleId]);
PRINT 'AspNetUserRoles table created.';

-- AspNetUserTokens Table
CREATE TABLE [dbo].[AspNetUserTokens] (
    [UserId] NVARCHAR(450) NOT NULL,
    [LoginProvider] NVARCHAR(450) NOT NULL,
    [Name] NVARCHAR(450) NOT NULL,
    [Value] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
PRINT 'AspNetUserTokens table created.';

-- SmtpSettings Table
CREATE TABLE [dbo].[SmtpSettings] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Server] NVARCHAR(MAX) NOT NULL,
    [Port] INT NOT NULL DEFAULT 587,
    [FromEmail] NVARCHAR(MAX) NOT NULL,
    [FromName] NVARCHAR(MAX) NOT NULL,
    [Username] NVARCHAR(MAX) NULL,
    [Password] NVARCHAR(MAX) NULL,
    [DefaultToRecipients] NVARCHAR(500) NULL,
    [DefaultCcRecipients] NVARCHAR(500) NULL,
    [EnableSsl] BIT NOT NULL DEFAULT 1,
    [EnableNotifications] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME2 NOT NULL,
    [ModifiedDate] DATETIME2 NOT NULL,
    CONSTRAINT [PK_SmtpSettings] PRIMARY KEY CLUSTERED ([Id] ASC)
);
PRINT 'SmtpSettings table created.';

-- EmailTemplates Table
CREATE TABLE [dbo].[EmailTemplates] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(MAX) NOT NULL,
    [TemplateType] NVARCHAR(MAX) NOT NULL,
    [Subject] NVARCHAR(MAX) NOT NULL,
    [Body] NVARCHAR(MAX) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME2 NOT NULL,
    [ModifiedDate] DATETIME2 NOT NULL,
    CONSTRAINT [PK_EmailTemplates] PRIMARY KEY CLUSTERED ([Id] ASC)
);
PRINT 'EmailTemplates table created.';

-- EF Migrations History Table
CREATE TABLE [dbo].[__EFMigrationsHistory] (
    [MigrationId] NVARCHAR(150) NOT NULL,
    [ProductVersion] NVARCHAR(32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC)
);
PRINT '__EFMigrationsHistory table created.';
GO

-- ============================================
-- INSERT SEED DATA
-- ============================================
PRINT 'Inserting seed data...';

-- Seed VisitUsers
SET IDENTITY_INSERT [dbo].[VisitUsers] ON;
INSERT INTO [dbo].[VisitUsers] ([Id], [FullName], [Email], [Role], [PhoneNumber], [AuthType], [LdapUserId], [CreatedDate], [IsActive])
VALUES 
    (1, 'John Doe', 'john.doe@example.com', 'Administrator', '+1-555-0100', 0, NULL, GETDATE(), 1),
    (2, 'Jane Smith', 'jane.smith@example.com', 'Sales Manager', '+1-555-0101', 0, NULL, GETDATE(), 1),
    (3, 'Bob Johnson', 'bob.johnson@example.com', 'Team Lead', '+1-555-0102', 0, NULL, GETDATE(), 1);
SET IDENTITY_INSERT [dbo].[VisitUsers] OFF;
PRINT 'Seeded 3 users into VisitUsers.';

-- Seed Visits
SET IDENTITY_INSERT [dbo].[Visits] ON;
INSERT INTO [dbo].[Visits] 
    ([Id], [SerialNumber], [TypeOfVisit], [Vertical], [SalesSpoc], [AccountName], [DebitingProjectId], 
     [OpportunityDetails], [OpportunityType], [ServiceScope], [SalesStage], [TcvMnUsd], [VisitStatus], 
     [VisitType], [VisitDate], [IntimationDate], [Location], [Site], [VisitorsName], [NumberOfAttendees], 
     [LevelOfVisitors], [VisitDuration], [Remarks], [VisitLead], [KeyMessages], [CreatedDate], [ModifiedDate], [CreatedBy])
VALUES 
    (1, 1, 'Client Meeting', 'Technology', 'John Doe', 'Acme Corporation', 'PROJ-001',
     'New cloud infrastructure project', 0, 'Cloud migration and consulting', 'Proposal', 1.5, 0,
     'On-site', '2024-01-15', '2024-01-08', 'New York', 'Acme HQ', 'Mark Williams, Sarah Davis', 5,
     'C-Level', '1 Day', 'Important strategic meeting', 'Capability', 'Demonstrate cloud capabilities and cost savings', 
     GETDATE(), NULL, 'admin@visitmanagement.com'),
    (2, 2, 'Technical Demo', 'Finance', 'Jane Smith', 'Global Bank Ltd', 'PROJ-002',
     'Digital transformation initiative', 1, 'Digital banking platform development', 'Negotiation', 3.2, 1,
     'Virtual', '2024-02-20', '2024-02-10', 'London', 'Global Bank Office', 'Peter Brown, Lisa Anderson', 8,
     'VP Level', '4 Hours', 'Follow-up demo session', 'Sales', 'Showcase platform scalability and security features',
     GETDATE(), NULL, 'admin@visitmanagement.com');
SET IDENTITY_INSERT [dbo].[Visits] OFF;
PRINT 'Seeded 2 visits into Visits.';

-- Seed EmailTemplates
SET IDENTITY_INSERT [dbo].[EmailTemplates] ON;
INSERT INTO [dbo].[EmailTemplates] ([Id], [Name], [TemplateType], [Subject], [Body], [IsActive], [CreatedDate], [ModifiedDate])
VALUES 
    (1, 'Visit Created Notification', 'VisitCreated', 'New Visit Created - {AccountName}', 
     '<html><body>
<h2>New Visit Has Been Created</h2>
<p>A new visit has been scheduled with the following details:</p>
<ul>
<li><strong>Account Name:</strong> {AccountName}</li>
<li><strong>Visit Date:</strong> {VisitDate}</li>
<li><strong>Location:</strong> {Location}</li>
<li><strong>Status:</strong> {VisitStatus}</li>
<li><strong>Sales SPOC:</strong> {SalesSpoc}</li>
<li><strong>Visit Type:</strong> {VisitType}</li>
<li><strong>Visitors:</strong> {VisitorsName}</li>
</ul>
<p>Please review the visit details in the system.</p>
<p>Best regards,<br/>Visit Management System</p>
</body></html>', 1, GETDATE(), GETDATE()),
    (2, 'Visit Updated Notification', 'VisitUpdated', 'Visit Updated - {AccountName}',
     '<html><body>
<h2>Visit Has Been Updated</h2>
<p>The visit details have been modified:</p>
<ul>
<li><strong>Account Name:</strong> {AccountName}</li>
<li><strong>Visit Date:</strong> {VisitDate}</li>
<li><strong>Location:</strong> {Location}</li>
<li><strong>Status:</strong> {VisitStatus}</li>
<li><strong>Sales SPOC:</strong> {SalesSpoc}</li>
<li><strong>Visit Type:</strong> {VisitType}</li>
<li><strong>Visitors:</strong> {VisitorsName}</li>
</ul>
<p>Please check the updated information in the system.</p>
<p>Best regards,<br/>Visit Management System</p>
</body></html>', 1, GETDATE(), GETDATE());
SET IDENTITY_INSERT [dbo].[EmailTemplates] OFF;
PRINT 'Seeded 2 email templates into EmailTemplates.';

-- Insert migration history
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES 
    ('20251229110538_InitialCreate', '8.0.0'),
    ('20251229123325_AddIdentity', '8.0.0'),
    ('20251229172318_AddSmtpAndEmailTemplates', '8.0.0'),
    ('20251230044524_AddUserAuthenticationAndSmtpRecipients', '8.0.0'),
    ('20251231120000_AddCreatedByToVisits', '8.0.0');
PRINT 'Migration history recorded.';
GO

-- ============================================
-- VERIFICATION
-- ============================================
PRINT '';
PRINT '============================================';
PRINT 'Database setup completed successfully!';
PRINT '============================================';
PRINT '';
PRINT 'Created Tables:';
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME;
PRINT '';
PRINT 'Row Counts:';
SELECT 'VisitUsers' AS TableName, COUNT(*) AS RowCount FROM [dbo].[VisitUsers]
UNION ALL
SELECT 'Visits', COUNT(*) FROM [dbo].[Visits]
UNION ALL
SELECT 'EmailTemplates', COUNT(*) FROM [dbo].[EmailTemplates]
UNION ALL
SELECT 'SmtpSettings', COUNT(*) FROM [dbo].[SmtpSettings]
UNION ALL
SELECT 'AspNetUsers', COUNT(*) FROM [dbo].[AspNetUsers];
PRINT '';
PRINT 'Next Steps:';
PRINT '1. Run the application: dotnet run';
PRINT '2. Login with default credentials:';
PRINT '   Email: admin@visitmanagement.com';
PRINT '   Password: Admin@123';
PRINT '';
PRINT 'Note: The default admin user will be created automatically on first run.';
PRINT '============================================';
GO
