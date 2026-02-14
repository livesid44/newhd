# SQL Server Setup Guide for Visit Management Application

This guide explains how to configure and use SQL Server with SQL Authentication (username and password) for the Visit Management application.

## Quick Start: Connection String Format

The application is now configured to use **local SQL Server with SQL Authentication** by default.

### Update Your Credentials

Open `appsettings.json` or `appsettings.Development.json` and replace `YOUR_USERNAME` and `YOUR_PASSWORD` with your actual SQL Server credentials:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=VisitManagement;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

## Connection String Options

Choose the appropriate connection string based on your SQL Server installation:

### 1. Default SQL Server Instance (localhost)

**When to use:** You installed SQL Server with the default instance name.

```
Server=localhost;Database=VisitManagement;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true
```

### 2. SQL Server Express (Named Instance)

**When to use:** You installed SQL Server Express (most common for development).

```
Server=localhost\SQLEXPRESS;Database=VisitManagement;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true
```

### 3. SQL Server LocalDB

**When to use:** You're using LocalDB (lightweight, developer-focused version).

```
Server=(localdb)\MSSQLLocalDB;Database=VisitManagement;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true
```

**Note:** LocalDB typically uses Windows Authentication, so you might prefer:
```
Server=(localdb)\MSSQLLocalDB;Database=VisitManagement;Integrated Security=True;TrustServerCertificate=True
```

### 4. Remote SQL Server

**When to use:** SQL Server is on a different machine or server.

```
Server=YOUR_SERVER_IP_OR_NAME;Database=VisitManagement;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true
```

## Setting Up SQL Server User (SQL Authentication)

### Step 1: Enable SQL Server Authentication

1. Open **SQL Server Management Studio (SSMS)**
2. Connect to your SQL Server instance
3. Right-click on the server name → **Properties**
4. Go to **Security** page
5. Under "Server authentication", select **"SQL Server and Windows Authentication mode"**
6. Click **OK**
7. **Restart SQL Server** for changes to take effect

### Step 2: Create SQL Server Login

Run this script in SSMS (replace `your_username` and `your_password`):

```sql
-- Create a new SQL Server login
CREATE LOGIN your_username WITH PASSWORD = 'your_password';
GO

-- Create a user in the master database
USE master;
CREATE USER your_username FOR LOGIN your_username;
GO
```

### Step 3: Create Database and Grant Permissions

Option A: Let the application create the database automatically (recommended):

```sql
-- Grant permission to create databases
ALTER SERVER ROLE dbcreator ADD MEMBER your_username;
GO
```

Then run your application - it will create the database automatically.

Option B: Create the database manually:

```sql
-- Create the database
CREATE DATABASE VisitManagement;
GO

-- Grant full access to the database
USE VisitManagement;
CREATE USER your_username FOR LOGIN your_username;
ALTER ROLE db_owner ADD MEMBER your_username;
GO
```

### Step 4: Verify Connection

Test the connection in SSMS:
1. File → Connect Object Explorer
2. Server name: `localhost` or `localhost\SQLEXPRESS`
3. Authentication: **SQL Server Authentication**
4. Login: `your_username`
5. Password: `your_password`
6. Click **Connect**

If successful, you can use these credentials in your application!

## Updating the Application Configuration

### For Development Environment

Edit `VisitManagement/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VisitManagement;User Id=your_username;Password=your_password;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

### For Production Environment

Edit `VisitManagement/appsettings.Production.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=VisitManagement;User Id=your_username;Password=your_password;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

## Running the Application

### First Time Setup

1. **Update connection string** with your credentials
2. **Run the application:**
   ```bash
   cd VisitManagement
   dotnet run
   ```
3. The application will:
   - Connect to SQL Server
   - Create the `VisitManagement` database automatically
   - Create all tables and schema
   - Seed initial data (admin user, roles, stakeholders)

### Using Migrations (Alternative)

If you prefer to use Entity Framework migrations:

```bash
# Apply all migrations to create/update database
dotnet ef database update

# Run the application
dotnet run
```

## Connection String Parameters Explained

| Parameter | Description | Example |
|-----------|-------------|---------|
| `Server` | SQL Server instance location | `localhost`, `localhost\SQLEXPRESS`, `192.168.1.100` |
| `Database` | Database name | `VisitManagement` |
| `User Id` | SQL Server username | `visitapp_user` |
| `Password` | SQL Server password | `YourSecurePassword123` |
| `TrustServerCertificate` | Accept self-signed certificates | `True` (for local development) |
| `MultipleActiveResultSets` | Enable MARS | `True` (recommended for EF Core) |
| `Integrated Security` | Use Windows Auth instead of SQL Auth | `True` (omit User Id/Password when using this) |

## Security Best Practices

### For Development

1. **Use User Secrets** instead of storing passwords in appsettings.json:
   ```bash
   cd VisitManagement
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost\SQLEXPRESS;Database=VisitManagement;User Id=your_username;Password=your_password;TrustServerCertificate=True;MultipleActiveResultSets=true"
   ```

2. **Restrict user permissions** - Grant only necessary database permissions

### For Production

1. **Use Environment Variables:**
   ```bash
   export ConnectionStrings__DefaultConnection="Server=..."
   ```

2. **Use Azure Key Vault or similar** for storing connection strings

3. **Enable SSL/TLS encryption:**
   ```
   Server=...;Encrypt=True;TrustServerCertificate=False;...
   ```

4. **Use strong passwords** (min 12 characters, mixed case, numbers, symbols)

5. **Limit user permissions** - Don't use `sa` or admin accounts

## Troubleshooting

### Error: "Login failed for user 'your_username'"

**Solutions:**
1. Verify SQL Server Authentication is enabled (see Step 1 above)
2. Check username and password are correct
3. Verify the login exists: `SELECT * FROM sys.sql_logins WHERE name = 'your_username'`
4. Ensure SQL Server service is running

### Error: "A network-related or instance-specific error"

**Solutions:**
1. Verify SQL Server is running: Open Services → Check "SQL Server (SQLEXPRESS)" status
2. Check server name is correct (use `localhost\SQLEXPRESS` not just `localhost`)
3. Enable TCP/IP protocol in SQL Server Configuration Manager
4. Check firewall settings (port 1433 for default instance)

### Error: "Cannot open database 'VisitManagement'"

**Solutions:**
1. Grant the user permission to create databases:
   ```sql
   ALTER SERVER ROLE dbcreator ADD MEMBER your_username;
   ```
2. Or create the database manually (see Step 3 above)

### Error: "Certificate chain was issued by an authority that is not trusted"

**Solution:**
Add `TrustServerCertificate=True` to your connection string.

### Verify Connection String

Test your connection string with this code:

```csharp
using Microsoft.Data.SqlClient;

var connectionString = "Server=localhost\\SQLEXPRESS;Database=master;User Id=your_username;Password=your_password;TrustServerCertificate=True";

try
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();
    Console.WriteLine("✅ Connection successful!");
    Console.WriteLine($"SQL Server version: {connection.ServerVersion}");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Connection failed: {ex.Message}");
}
```

## Example Configurations

### Example 1: Local SQL Server Express with SQL Auth

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VisitManagement;User Id=visitapp;Password=Visit@2024!Secure;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

### Example 2: Local Default Instance with SQL Auth

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=VisitManagement;User Id=visitapp;Password=Visit@2024!Secure;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

### Example 3: Remote SQL Server with SQL Auth

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=192.168.1.100,1433;Database=VisitManagement;User Id=visitapp;Password=Visit@2024!Secure;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

### Example 4: LocalDB with Windows Auth (No password needed)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=VisitManagement;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

## Next Steps

After configuring your connection string:

1. **Update credentials** in `appsettings.json` or use User Secrets
2. **Run the application**: `dotnet run`
3. **Access the application** at https://localhost:5001
4. **Login with default admin:**
   - Email: `admin@techmahindra.com`
   - Password: `Admin@123`

## Additional Resources

- [SQL Server Downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core User Secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)

## Support

If you encounter issues not covered in this guide, please check:
- SQL Server error logs
- Application logs (`dotnet run` output)
- Windows Event Viewer (for SQL Server service issues)
