# Database Setup Guide

This document explains how the Visit Management application stores data and how to configure database connections.

## Current Database Configuration

The application uses **Entity Framework Core** with support for both SQLite and SQL Server databases.

### Auto-Detection Logic

The application automatically detects which database provider to use based on the connection string (see `Program.cs` lines 13-24):

- **If connection string contains `.db`** → Uses SQLite
- **Otherwise** → Uses SQL Server

## Database Options

### Option 1: SQLite (Default for Development)

**Advantages:**
- ✅ No SQL Server installation required
- ✅ File-based, portable database
- ✅ Great for development and testing
- ✅ Simple setup - just run the application

**Current Configuration:**
```json
"DefaultConnection": "Data Source=visitmanagement.db"
```

**Database Location:** 
- File: `VisitManagement/visitmanagement.db`
- Created automatically when you run the application
- Stored in the project directory

**How to Use:**
1. No additional setup required
2. Run the application: `dotnet run`
3. Database is created automatically on first run
4. Data persists in `visitmanagement.db` file

### Option 2: SQL Server (Recommended for Production)

**Advantages:**
- ✅ Better performance for production
- ✅ Advanced features (replication, backup, security)
- ✅ Scalable for multiple users
- ✅ Industry-standard enterprise database

**Prerequisites:**
- SQL Server 2016 or later
- Or SQL Server Express (free)
- Or Azure SQL Database

## Switching to SQL Server

### Step 1: Update Connection String

Edit the appropriate `appsettings.json` file:

**For Development** (`appsettings.Development.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VisitManagement;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

**For Production** (`appsettings.Production.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=VisitManagement;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

### Step 2: Create Database

The database is created automatically when you run the application (using `context.Database.EnsureCreated()` in `Program.cs` line 74).

Alternatively, you can use migrations:

```bash
# Apply migrations to create database and schema
dotnet ef database update
```

### Step 3: Run Application

```bash
dotnet run
```

The application will:
1. Connect to SQL Server
2. Create the database if it doesn't exist
3. Create all tables and schema
4. Seed initial data (roles, admin user, stakeholders)

## Connection String Examples

### Local SQL Server Express (Windows Authentication)
```
Server=.\\SQLEXPRESS;Database=VisitManagement;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true
```

### SQL Server with SQL Authentication
```
Server=YOUR_SERVER_NAME;Database=VisitManagement;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true
```

### Azure SQL Database
```
Server=tcp:yourserver.database.windows.net,1433;Database=VisitManagement;User ID=yourusername@yourserver;Password=yourpassword;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

### Local SQL Server (default instance)
```
Server=localhost;Database=VisitManagement;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true
```

### Remote SQL Server
```
Server=192.168.1.100,1433;Database=VisitManagement;User Id=sa;Password=YourPassword;TrustServerCertificate=True;MultipleActiveResultSets=true
```

## Security Best Practices

### 1. Never Commit Passwords to Git

For production, use one of these secure methods:

**Option A: User Secrets (Development)**
```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=...;Password=SecurePassword"
```

**Option B: Environment Variables**
```bash
# Windows
set ConnectionStrings__DefaultConnection=Server=...;Password=SecurePassword

# Linux/Mac
export ConnectionStrings__DefaultConnection="Server=...;Password=SecurePassword"
```

**Option C: Azure App Service Configuration**
- Go to Azure Portal → App Service → Configuration
- Add connection string in "Connection strings" section
- Select "SQLServer" as type

### 2. Use Windows Authentication When Possible

For internal deployments on Windows:
```
Server=YOUR_SERVER;Database=VisitManagement;Integrated Security=True;TrustServerCertificate=True
```

### 3. Restrict Database User Permissions

Create a dedicated SQL user with minimal permissions:
```sql
CREATE LOGIN VisitManagementUser WITH PASSWORD = 'StrongPassword123!';
CREATE USER VisitManagementUser FOR LOGIN VisitManagementUser;
ALTER ROLE db_datareader ADD MEMBER VisitManagementUser;
ALTER ROLE db_datawriter ADD MEMBER VisitManagementUser;
```

## Database Schema

The application uses Entity Framework Code-First approach. The schema includes:

**Tables:**
- `AspNetUsers` - User accounts
- `AspNetRoles` - User roles (Admin, User)
- `AspNetUserRoles` - User-role mappings
- `Visits` - Visit records
- `TaskAssignments` - Team task assignments
- `Checklists` - Visit checklists
- `Stakeholders` - Stakeholder directory

**Migrations:**
All schema changes are tracked in the `Migrations` folder. To apply:
```bash
dotnet ef database update
```

## Troubleshooting

### Issue: "Cannot open database"
**Solution:** Ensure SQL Server is running and connection string is correct.

### Issue: "Login failed for user"
**Solution:** Check username/password or use Windows Authentication.

### Issue: "Server not found"
**Solution:** Verify server name, check firewall, ensure SQL Server is accessible.

### Issue: SQLite file locked
**Solution:** Close the application and any database browsers. Delete `.db-shm` and `.db-wal` files if they exist.

### Issue: Migration errors
**Solution:** 
```bash
# Drop database and recreate
dotnet ef database drop
dotnet ef database update
```

## Viewing Data

### SQLite
Use tools like:
- DB Browser for SQLite (https://sqlitebrowser.org/)
- VS Code SQLite extension
- Azure Data Studio with SQLite extension

### SQL Server
Use tools like:
- SQL Server Management Studio (SSMS)
- Azure Data Studio
- Visual Studio Server Explorer
- VS Code with SQL Server extension

## Production Deployment Checklist

- [ ] Configure SQL Server connection string in production environment
- [ ] Use secure credential storage (not in appsettings.json)
- [ ] Apply database migrations: `dotnet ef database update`
- [ ] Create database backups
- [ ] Configure SQL Server firewall rules
- [ ] Test connection before deployment
- [ ] Enable SSL/TLS for connections
- [ ] Review SQL Server security settings
- [ ] Set up monitoring and alerts

## Need Help?

- **Entity Framework Core Docs:** https://docs.microsoft.com/ef/core/
- **SQL Server Connection Strings:** https://www.connectionstrings.com/sql-server/
- **Azure SQL Database:** https://docs.microsoft.com/azure/sql-database/

## Default Admin Credentials

After first run, login with:
- **Email:** admin@visitmanagement.com
- **Password:** Admin@123

**⚠️ Important:** Change the admin password immediately after first login!
