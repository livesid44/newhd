# Troubleshooting Guide

## Common Issues and Solutions

### 1. SQL Server Connection Error

**Error Message:**
```
Microsoft.Data.SqlClient.SqlException: A network-related or instance-specific error occurred 
while establishing a connection to SQL Server. The server was not found or was not accessible.
Error Locating Server/Instance Specified
```

**Cause:**
This error occurs when the application cannot connect to SQL Server. Common reasons:
- SQL Server is not running
- Incorrect server name in connection string
- SQL Server not configured to accept remote connections
- Wrong username/password
- Connection string still has placeholders (`YOUR_USERNAME`, `YOUR_PASSWORD`)

**Solution Option 1: Use SQLite (Recommended for Development)**

SQLite requires no installation and works out of the box!

1. Open `appsettings.Development.json`
2. Verify the connection string is:
   ```json
   "DefaultConnection": "Data Source=visitmanagement.db"
   ```
3. Run the application - it will work immediately!

**Solution Option 2: Fix SQL Server Connection**

If you need to use SQL Server:

1. **Verify SQL Server is running:**
   - Open Services (services.msc)
   - Find "SQL Server (MSSQLSERVER)" or "SQL Server (SQLEXPRESS)"
   - Ensure it's "Running"
   - If not, right-click and select "Start"

2. **Update connection string:**
   - Open `appsettings.json` or `appsettings.Development.json`
   - Replace placeholders with actual values:
     ```json
     "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VisitManagement;User Id=YOUR_ACTUAL_USERNAME;Password=YOUR_ACTUAL_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true"
     ```

3. **Test SQL Server connection:**
   - Open SQL Server Management Studio (SSMS)
   - Try connecting with the same credentials
   - If you can't connect in SSMS, the app won't connect either

4. **Common server names:**
   - SQL Server Express: `localhost\SQLEXPRESS`
   - Default instance: `localhost` or `.`
   - LocalDB: `(localdb)\MSSQLLocalDB`
   - Named instance: `localhost\YOUR_INSTANCE_NAME`

5. **Enable SQL Server Authentication:**
   - Open SQL Server Configuration Manager
   - SQL Server Network Configuration → Protocols
   - Enable TCP/IP
   - Restart SQL Server service

For detailed SQL Server setup, see [SQL_SERVER_SETUP.md](SQL_SERVER_SETUP.md)

---

### 2. Database Migration Errors

**Error:** "Pending model changes" or migration errors

**Solution:**
```bash
cd VisitManagement
dotnet ef database update
```

Or delete the database and let it recreate:
- SQLite: Delete `visitmanagement.db` file
- SQL Server: Drop the database in SSMS

---

### 3. Login Issues

**Problem:** Cannot login with default admin account

**Default Credentials:**
- Email: `admin@visitmanagement.com`
- Password: `Admin@123`

**Solution:**
1. Ensure database was seeded properly (check application logs on startup)
2. Try resetting the database:
   - SQLite: Delete `visitmanagement.db` and restart app
   - SQL Server: Drop database and restart app

---

### 4. "Database already exists" Error

**Solution:**

For SQLite:
```bash
cd VisitManagement
rm visitmanagement.db
dotnet run
```

For SQL Server:
```sql
-- Run in SSMS
DROP DATABASE VisitManagement;
```

Then restart the application.

---

### 5. Port Already in Use

**Error:** "Failed to bind to address... port 5000 already in use"

**Solution:**
```bash
# Find process using port 5000
netstat -ano | findstr :5000  # Windows
lsof -i :5000                  # Mac/Linux

# Kill the process or use a different port in launchSettings.json
```

---

### 6. NuGet Package Restore Issues

**Error:** "Package restore failed"

**Solution:**
```bash
cd VisitManagement
dotnet restore
dotnet build
```

---

### 7. Chart.js Not Loading

**Problem:** Dashboard charts not displaying

**Solution:**
1. Check browser console for errors (F12)
2. Ensure internet connection (Chart.js loads from CDN)
3. Try clearing browser cache
4. Check if Content Security Policy is blocking scripts

---

### 8. Email Sending Failures

**Problem:** Emails not being sent

**Solution:**
1. Check SMTP settings in database (SmtpSettings table)
2. Verify SMTP server is accessible
3. Check firewall settings
4. Review application logs for detailed error messages
5. Test SMTP settings with a mail client

---

### 9. Permission Denied Errors

**Problem:** User cannot access certain features

**Solution:**
1. Verify user role:
   - Admin users: Full access
   - Regular users: Limited access
2. Check user's role assignment in database
3. Log out and log back in to refresh permissions

---

### 10. Development Certificate Errors

**Error:** "Unable to configure HTTPS endpoint"

**Solution:**
```bash
# Trust the development certificate
dotnet dev-certs https --trust

# Or clean and reinstall
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

---

## Getting Help

1. **Check Logs:**
   - Development: Console output shows detailed logs
   - Production: Check application logs in hosting environment

2. **Documentation:**
   - [SQL_SERVER_SETUP.md](SQL_SERVER_SETUP.md) - SQL Server configuration
   - [DATABASE_SETUP.md](DATABASE_SETUP.md) - Database options
   - [QUICKSTART_SQL.md](QUICKSTART_SQL.md) - Quick SQL Server setup
   - [README.md](README.md) - General setup and usage

3. **Common Commands:**
   ```bash
   # Build the project
   dotnet build
   
   # Run the application
   dotnet run
   
   # Run migrations
   dotnet ef database update
   
   # Create new migration
   dotnet ef migrations add MigrationName
   
   # Clean build
   dotnet clean
   dotnet build
   ```

4. **Database Tools:**
   - SQLite: DB Browser for SQLite (https://sqlitebrowser.org/)
   - SQL Server: SQL Server Management Studio (SSMS)

5. **Reset Everything:**
   ```bash
   # Clean project
   dotnet clean
   
   # Delete database
   rm visitmanagement.db  # SQLite
   
   # Restore and rebuild
   dotnet restore
   dotnet build
   dotnet run
   ```
