# Quick Start - Database Setup

## If you're having EF migration issues

### Step 1: Verify SQL Server is running

For LocalDB:
```bash
sqllocaldb info
sqllocaldb start MSSQLLocalDB
```

### Step 2: Run the setup script

**Option 1: Using sqlcmd (Command Line)**
```bash
# From the repository root
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -i Database/Setup-Database.sql
```

**Option 2: Using SQL Server Management Studio**
1. Open SSMS
2. Connect to: `(localdb)\MSSQLLocalDB`
3. Open File → `Database/Setup-Database.sql`
4. Press F5 to execute

**Option 3: Using Azure Data Studio**
1. Open Azure Data Studio
2. Connect to: `(localdb)\MSSQLLocalDB`
3. Open File → `Database/Setup-Database.sql`
4. Click Run or press F5

### Step 3: Run the application

```bash
cd VisitManagement
dotnet run
```

### Step 4: Login

- URL: `http://localhost:5000` or `https://localhost:5001`
- Email: `admin@visitmanagement.com`
- Password: `Admin@123`

## What gets created?

✅ Database: `VisitManagementDB`
✅ 12 Tables (including ASP.NET Identity tables)
✅ 3 Sample Users in VisitUsers
✅ 2 Sample Visits
✅ 2 Email Templates
✅ Migration history

## Troubleshooting

**Error: "Login failed for user"**
- Use Windows Authentication (`-E` flag)
- Or use SQL Authentication: `sqlcmd -S server -U username -P password -i Database/Setup-Database.sql`

**Error: "Cannot open database"**
- The script creates the database automatically
- Ensure SQL Server service is running

**Error: "Invalid object name"**
- The script drops and recreates all tables
- This is normal and expected

## Need more help?

See the full documentation: [Database/README.md](README.md)
