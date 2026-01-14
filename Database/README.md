# Database Setup Scripts

This folder contains SQL scripts to set up the Visit Management System database without using Entity Framework migrations.

## Overview

If you're experiencing issues with EF migrations (`dotnet ef database update`), you can use these SQL scripts as an alternative to manually create the database, tables, and seed data.

## Files

- **Setup-Database.sql**: Complete end-to-end script that creates all tables and inserts seed data

## Prerequisites

- SQL Server 2016 or later
- SQL Server Management Studio (SSMS) or Azure Data Studio
- Or any SQL client that can execute T-SQL scripts

## Option 1: Using SQL Server Management Studio (SSMS)

1. **Open SQL Server Management Studio**

2. **Connect to your SQL Server instance**
   - For LocalDB: Use server name `(localdb)\MSSQLLocalDB`
   - For SQL Server: Use your server instance name

3. **Open the script**
   - File → Open → File
   - Navigate to `Database/Setup-Database.sql`

4. **Execute the script**
   - Press F5 or click "Execute"
   - The script will:
     - Create the database `VisitManagementDB` if it doesn't exist
     - Drop existing tables (if re-running)
     - Create all required tables
     - Insert seed data
     - Record migration history

5. **Verify the results**
   - Check the Messages tab for success messages
   - The script displays created tables and row counts

## Option 2: Using sqlcmd Command Line

```bash
# For LocalDB
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -i Database/Setup-Database.sql

# For SQL Server with Windows Authentication
sqlcmd -S YOUR_SERVER_NAME -E -i Database/Setup-Database.sql

# For SQL Server with SQL Authentication
sqlcmd -S YOUR_SERVER_NAME -U YOUR_USERNAME -P YOUR_PASSWORD -i Database/Setup-Database.sql
```

## Option 3: Using Azure Data Studio

1. **Open Azure Data Studio**

2. **Connect to your SQL Server**

3. **Open the script**
   - File → Open File
   - Select `Database/Setup-Database.sql`

4. **Run the script**
   - Click "Run" or press F5

## What Gets Created

### Tables

1. **VisitUsers** - System users for visit management
   - Fields: Id, FullName, Email, Role, PhoneNumber, AuthType, LdapUserId, CreatedDate, IsActive

2. **Visits** - Visit records with comprehensive details (28 fields)
   - Core: Id, SerialNumber, TypeOfVisit, Vertical, AccountName
   - Opportunity: OpportunityDetails, OpportunityType (NN/EN), ServiceScope, SalesStage, TcvMnUsd
   - Visit Details: VisitDate, IntimationDate, VisitStatus, VisitType, Location, Site
   - Attendees: VisitorsName, NumberOfAttendees, LevelOfVisitors, VisitDuration
   - Additional: VisitLead, KeyMessages, Remarks, SalesSpoc, DebitingProjectId
   - **Audit**: CreatedDate, ModifiedDate, **CreatedBy** (newly added field)

3. **AspNetUsers** - Identity framework users for authentication
4. **AspNetRoles** - User roles
5. **AspNetUserClaims** - User claims
6. **AspNetUserLogins** - External login providers
7. **AspNetUserRoles** - User-role relationships
8. **AspNetUserTokens** - Authentication tokens
9. **AspNetRoleClaims** - Role claims
10. **SmtpSettings** - Email configuration
11. **EmailTemplates** - Email notification templates
12. **__EFMigrationsHistory** - Migration tracking table

### Seed Data

- **3 sample users** in VisitUsers table
- **2 sample visits** in Visits table
- **2 email templates** for notifications

## Database Schema Details

### Visits Table (Main Entity)

- Comprehensive visit tracking with 27 fields
- Includes account details, opportunity information, visitor data
- **Note**: Includes `CreatedBy` field (this was missing in the original migrations)

### Enumerations

- **OpportunityType**: 0 = NN, 1 = EN
- **VisitStatus**: 0 = Confirmed, 1 = Tentative
- **AuthType**: 0 = Password, 1 = LDAP

## Updating Connection String

If you're not using the default LocalDB, update the connection string in `VisitManagement/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=VisitManagementDB;Integrated Security=True;..."
  }
}
```

## After Running the Script

1. **Update your connection string** in `appsettings.json` if needed

2. **Run the application**:
   ```bash
   cd VisitManagement
   dotnet run
   ```

3. **Login with default admin account**:
   - Email: `admin@visitmanagement.com`
   - Password: `Admin@123`
   
   Note: The admin account is created automatically by the application on first run.

## Troubleshooting

### Script fails with "Database already exists"

The script is designed to handle existing databases. It will:
- Use the existing database
- Drop and recreate all tables
- Re-insert seed data

### Permission errors

Ensure your SQL Server user has:
- CREATE DATABASE permission (for first-time setup)
- db_owner role on VisitManagementDB database

### Connection timeout

For LocalDB, ensure the SQL Server LocalDB service is running:
```bash
sqllocaldb start MSSQLLocalDB
```

## Re-running the Script

The script is idempotent and can be safely re-run. It will:
1. Drop all existing tables
2. Recreate them with the latest schema
3. Re-insert seed data

**Warning**: This will delete all existing data in the tables!

## Alternative: Using EF Migrations

If you prefer to use EF migrations after all, you need to:

1. Install EF tools:
   ```bash
   dotnet tool install --global dotnet-ef --version 8.0.0
   ```

2. Add a new migration for the missing CreatedBy field:
   ```bash
   cd VisitManagement
   dotnet ef migrations add AddCreatedByToVisits
   ```

3. Update the database:
   ```bash
   dotnet ef database update
   ```

However, if migrations are problematic, the SQL script approach is recommended.

## Support

For issues or questions, please open an issue on GitHub.
