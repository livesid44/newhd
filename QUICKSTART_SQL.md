# Quick Start: SQL Server Connection String Setup

## What Changed

The application is now configured to use **local SQL Server with SQL Authentication** (username and password).

## Your Next Steps

### Step 1: Update Your Connection String

Open `VisitManagement/appsettings.json` and replace the placeholders:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VisitManagement;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

**Replace:**
- `YOUR_USERNAME` → Your actual SQL Server username
- `YOUR_PASSWORD` → Your actual SQL Server password

### Step 2: Choose the Right Server Name

Pick the connection string that matches your SQL Server installation:

#### Option A: SQL Server Express (Most Common)
```
Server=localhost\SQLEXPRESS;Database=VisitManagement;User Id=youruser;Password=yourpass;TrustServerCertificate=True;MultipleActiveResultSets=true
```

#### Option B: Default SQL Server Instance
```
Server=localhost;Database=VisitManagement;User Id=youruser;Password=yourpass;TrustServerCertificate=True;MultipleActiveResultSets=true
```

#### Option C: LocalDB
```
Server=(localdb)\MSSQLLocalDB;Database=VisitManagement;User Id=youruser;Password=yourpass;TrustServerCertificate=True;MultipleActiveResultSets=true
```

#### Option D: Windows Authentication (No Password Needed)
```
Server=localhost\SQLEXPRESS;Database=VisitManagement;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true
```

### Step 3: Set Up SQL Server User (If Needed)

If you don't have a SQL Server user yet, follow these steps in SQL Server Management Studio (SSMS):

1. **Enable SQL Authentication:**
   - Right-click server → Properties → Security
   - Select "SQL Server and Windows Authentication mode"
   - Restart SQL Server service

2. **Create Login:**
   ```sql
   CREATE LOGIN youruser WITH PASSWORD = 'yourpassword';
   ALTER SERVER ROLE dbcreator ADD MEMBER youruser;
   ```

3. **Run the application** - it will create the database automatically!

### Step 4: Run the Application

```bash
cd VisitManagement
dotnet run
```

The application will:
- Connect to your SQL Server
- Create the `VisitManagement` database automatically
- Create all tables
- Seed initial data

### Step 5: Access the Application

- **URL:** https://localhost:5001
- **Login:** admin@techmahindra.com
- **Password:** Admin@123

## Need More Help?

📘 **Complete Setup Guide:** See [SQL_SERVER_SETUP.md](SQL_SERVER_SETUP.md)
- Step-by-step SQL Server configuration
- Troubleshooting common errors
- Security best practices

## Quick Troubleshooting

### "Login failed for user"
- Verify SQL Authentication is enabled
- Check username and password are correct
- Ensure SQL Server service is running

### "A network-related error"
- Check server name (use `localhost\SQLEXPRESS` not just `localhost`)
- Verify SQL Server service is running (Services → SQL Server (SQLEXPRESS))

### "Cannot open database"
- Grant the user dbcreator role
- Or the app will create it automatically on first run

## Example Working Connection String

For SQL Server Express with a user named `visitapp`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VisitManagement;User Id=visitapp;Password=Visit@2024;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

**Note:** Remember to use `\\` for the backslash in JSON (escape sequence).

## That's It!

You're all set. Just update the connection string and run the app! 🚀
