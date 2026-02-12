# Visit Management System

A comprehensive ASP.NET Core MVC application for managing client visits and user information, built with Entity Framework Core and SQL Server.

## Features

### Visit Management
- **Comprehensive Visit Tracking**: Log and manage detailed visit information including:
  - Visit type, vertical, and sales SPOC
  - Account details and project information
  - Opportunity details and type (NN/EN)
  - Visit status (Confirmed/Tentative)
  - Visit date with calendar picker
  - Location and site information
  - Visitor details and attendee count
  - Visit duration and key messages
  
- **Search & Filter**: Advanced search and filtering capabilities across visit data
- **Sorting**: Sort visits by account name, visit date, and status
- **Full CRUD Operations**: Create, Read, Update, and Delete visit records

### User Management
- **User Registration**: Add and manage system users
- **User Profiles**: Track user details including name, email, role, and phone number
- **User Status**: Activate or deactivate user accounts
- **Search Functionality**: Quick search across user data

### Technical Features
- **Enterprise-Level Authentication**: ASP.NET Core Identity with secure login/register
- **Role-Based Access Control**: Protected routes requiring authentication
- **Secure Password Requirements**: 8+ characters with uppercase, lowercase, digit, and special character
- **Account Security**: Automatic lockout after 5 failed login attempts
- **Session Management**: Secure 8-hour cookie-based sessions
- Responsive design using Bootstrap 5
- Data validation with DataAnnotations
- Entity Framework Core with Code-First approach
- SQL Server database with proper normalization
- Seed data for testing and demonstration
- Comprehensive unit tests for controllers
- Clean architecture with separation of concerns

## Prerequisites

- .NET 8.0 SDK or later
- **SQL Server** (choose one):
  - SQL Server 2016+ (any edition)
  - SQL Server Express (free) - recommended for development
  - LocalDB (included with Visual Studio)
  - Azure SQL Database
- Visual Studio 2022 / Visual Studio Code / Rider (optional)
- SQL Server Management Studio (SSMS) - recommended for database management

**Note:** The application supports both SQL Server (with SQL Authentication or Windows Authentication) and SQLite. See [SQL_SERVER_SETUP.md](SQL_SERVER_SETUP.md) for configuration details.

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/livesid44/newhd.git
cd newhd
```

### 2. Build the Solution

```bash
dotnet build
```

### 3. Configure Database Connection

**The application is configured to use SQL Server with SQL Authentication by default.**

**IMPORTANT:** Update the connection string in `appsettings.json` or `appsettings.Development.json` with your SQL Server credentials:

```json
"DefaultConnection": "Server=localhost;Database=VisitManagement;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

**Common Connection String Formats:**

- **SQL Server Express**: `Server=localhost\\SQLEXPRESS;Database=VisitManagement;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true`
- **Default Instance**: `Server=localhost;Database=VisitManagement;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true`
- **Windows Auth** (no password): `Server=localhost;Database=VisitManagement;Integrated Security=True;TrustServerCertificate=True`

**📘 Need help setting up SQL Server?**
- **See [SQL_SERVER_SETUP.md](SQL_SERVER_SETUP.md)** for complete step-by-step instructions including:
  - Creating SQL Server users
  - Granting permissions
  - Connection string examples
  - Troubleshooting guide

**Alternative: Use SQLite** (no SQL Server needed):
```json
"DefaultConnection": "Data Source=visitmanagement.db"
```
See [DATABASE_SETUP.md](DATABASE_SETUP.md) for more database options.

The application auto-detects the database type:
- Connection string with `.db` → SQLite
- Otherwise → SQL Server

### 4. Create Database and Apply Schema

The database is created automatically when you run the application for the first time. No manual steps required!

**Optional**: If you want to use Entity Framework migrations explicitly:

```bash
cd VisitManagement
dotnet ef database update
```

This will create the database with all tables and seed data.

### 5. Run the Application

```bash
dotnet run
```

The application will start and be accessible at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

### 6. Login with Default Admin Account

The application automatically creates a default administrator account on first run. Use these credentials to login:

- **Email**: `admin@visitmanagement.com`
- **Password**: `Admin@123`

On first run, you'll be redirected to the login page. Enter the credentials above to access the system.

## Authentication

The application uses ASP.NET Core Identity for secure authentication:

- **Default Admin User**: Pre-configured admin account created automatically on startup
  - Email: `admin@visitmanagement.com`
  - Password: `Admin@123`
- **Login**: Access at `/Account/Login` or click "Login" in the navigation
- **Logout**: Click on your username dropdown and select "Logout"
- **Protected Routes**: Visit and User management pages require authentication
- **No Public Registration**: Registration is disabled to prevent unauthorized access
- **Security Features**:
  - Secure password hashing
  - Account lockout after 5 failed login attempts (15-minute lockout)
  - HttpOnly cookies for session management
  - 8-hour session timeout with sliding expiration

## Running Tests

Execute the unit tests with:

```bash
cd VisitManagement.Tests
dotnet test
```

All tests should pass, validating the core functionality of the controllers.

## Project Structure

```
newhd/
├── VisitManagement/              # Main application
│   ├── Controllers/              # MVC Controllers
│   │   ├── AccountController.cs   # Authentication
│   │   ├── VisitsController.cs
│   │   └── UsersController.cs
│   ├── Data/                     # Database context
│   │   └── ApplicationDbContext.cs
│   ├── Models/                   # Data models
│   │   ├── ApplicationUser.cs     # Identity user model
│   │   ├── Visit.cs
│   │   └── User.cs
│   ├── ViewModels/               # View models
│   │   ├── LoginViewModel.cs
│   │   └── RegisterViewModel.cs
│   ├── Views/                    # Razor views
│   │   ├── Account/               # Login/Register
│   │   ├── Visits/
│   │   ├── Users/
│   │   └── Shared/
│   └── wwwroot/                  # Static files
├── VisitManagement.Tests/        # Unit tests
│   ├── VisitsControllerTests.cs
│   └── UsersControllerTests.cs
└── VisitManagement.sln           # Solution file
```

## Database Schema

### Identity Tables
ASP.NET Core Identity creates the following tables:
- **AspNetUsers**: Authenticated users with email, password hash, and security stamps
- **AspNetRoles**: User roles (if role-based access is configured)
- **AspNetUserRoles**: Many-to-many relationship between users and roles
- **AspNetUserClaims**, **AspNetUserLogins**, **AspNetUserTokens**: Additional Identity features

### Visits Table
Stores comprehensive visit information with fields including:
- Visit details (type, date, duration)
- Account and project information
- Opportunity details
- Sales and visitor information
- Status tracking

### Users Table
Manages user information:
- Personal details (name, email, phone)
- Role and status
- Creation date tracking

## Usage

### First Time Setup

1. **Run the application** and you'll be redirected to the login page
2. **Login with the default admin account**:
   - Email: `admin@visitmanagement.com`
   - Password: `Admin@123`
3. **You'll be automatically logged in** and redirected to the Visits page

### Managing Visits

1. **View All Visits**: Navigate to the home page to see all visits
2. **Add New Visit**: Click "Add New Visit" and fill in the required information
3. **Edit Visit**: Click the edit icon on any visit to update details
4. **View Details**: Click the details icon to see complete visit information
5. **Delete Visit**: Click the delete icon to remove a visit (with confirmation)
6. **Search**: Use the search box to find specific visits
7. **Sort**: Click column headers to sort by different criteria

### Managing Users

1. **View All Users**: Navigate to "Users" in the navigation menu
2. **Add New User**: Click "Add New User" and provide user details
3. **Edit User**: Update user information including activation status
4. **View Details**: See complete user profile information
5. **Delete User**: Remove users from the system

## Technologies Used

- **ASP.NET Core 8.0 MVC**: Web framework
- **Entity Framework Core 8.0**: ORM for database operations
- **SQL Server**: Primary database (supports 2016+, Express, LocalDB, Azure SQL)
- **SQLite**: Alternative lightweight database option
- **ASP.NET Core Identity**: Authentication and user management
- **Bootstrap 5**: Responsive UI framework
- **Bootstrap Icons**: Icon library
- **Chart.js 4.4.0**: Interactive charts and visualizations
- **xUnit**: Unit testing framework
- **Moq**: Mocking library for tests

## Validation

All forms include comprehensive client-side and server-side validation:
- Required field validation
- Email format validation
- Phone number validation
- Date validation
- Numeric range validation

## Security Considerations

- Anti-forgery tokens on all POST operations
- Model binding validation
- Input sanitization through DataAnnotations
- Proper error handling and display

## Future Enhancements

Potential improvements for future versions:
- User authentication and authorization
- Role-based access control
- Export functionality (Excel, PDF)
- Advanced reporting and analytics
- Email notifications for visit confirmations
- Calendar integration
- Mobile app

## Troubleshooting

### Database Configuration

**Question: Where is the data stored?**

The application is configured to use **SQL Server with SQL Authentication** by default.

- **Database Server**: Your local SQL Server instance (localhost, localhost\SQLEXPRESS, or (localdb)\MSSQLLocalDB)
- **Database Name**: `VisitManagement`
- **Authentication**: SQL Server Authentication (username and password)
- **Created automatically**: Database and tables are created when you first run the application

**Connection String Location**: 
- Development: `VisitManagement/appsettings.Development.json`
- Production: `VisitManagement/appsettings.Production.json`

**📘 Complete Setup Guide**: See [SQL_SERVER_SETUP.md](SQL_SERVER_SETUP.md) for:
- How to create SQL Server users
- Connection string examples
- Troubleshooting common issues
- Security best practices

**Alternative**: You can switch to SQLite (no SQL Server needed) by changing the connection string to:
```json
"DefaultConnection": "Data Source=visitmanagement.db"
```
See [DATABASE_SETUP.md](DATABASE_SETUP.md) for more options.

### Database Errors

**Error: "Login failed for user 'YOUR_USERNAME'"**

1. **Update credentials** in `appsettings.json`:
   ```json
   "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VisitManagement;User Id=YOUR_ACTUAL_USERNAME;Password=YOUR_ACTUAL_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true"
   ```

2. **Verify SQL Authentication is enabled** - See [SQL_SERVER_SETUP.md](SQL_SERVER_SETUP.md) Step 1

3. **Check SQL Server is running**:
   - Open Services (services.msc)
   - Find "SQL Server (SQLEXPRESS)" or "SQL Server (MSSQLSERVER)"
   - Ensure status is "Running"

**Error: "Invalid object name 'Visits'"**

The database hasn't been created yet. Follow these steps:

1. **Verify connection string** is correct in appsettings.json

2. **Run the application** (database is created automatically):
   ```bash
   cd VisitManagement
   dotnet run
   ```

3. **Alternative - Use migrations** (if automatic creation fails):
   ```bash
   dotnet tool install --global dotnet-ef --version 8.0.0
   cd VisitManagement
   dotnet ef database update
   ```

### Decimal Precision Warning

The warning about `TcvMnUsd` decimal precision has been resolved in the latest version. The property is now configured with `decimal(18,2)` precision in the database schema. If you see this warning:

1. Ensure you're using the latest version of the code
2. Remove old migrations if any: `dotnet ef migrations remove`
3. Create a fresh migration: `dotnet ef migrations add InitialCreate`
4. Update the database: `dotnet ef database update`

### Connection String Examples

**SQL Server Express (most common for development)**:
```json
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VisitManagement;User Id=visitapp;Password=YourPassword123;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

**Default SQL Server Instance**:
```json
"DefaultConnection": "Server=localhost;Database=VisitManagement;User Id=visitapp;Password=YourPassword123;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

**Windows Authentication** (no password needed):
```json
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VisitManagement;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

**SQLite** (alternative, no SQL Server needed):
```json
"DefaultConnection": "Data Source=visitmanagement.db"
```

For more connection string examples and troubleshooting, see [SQL_SERVER_SETUP.md](SQL_SERVER_SETUP.md).
```json
"DefaultConnection": "Data Source=visitmanagement.db"
```

The application automatically detects the database type:
- If connection string contains `.db` → Uses SQLite
- Otherwise → Uses SQL Server

**Switching to SQL Server**:
1. Update `appsettings.json` with SQL Server connection string:
```json
"DefaultConnection": "Server=YOUR_SERVER;Database=VisitManagement;Integrated Security=True;TrustServerCertificate=True"
```
2. Run the application - database is created automatically

For detailed SQL Server configuration options, see [DATABASE_SETUP.md](DATABASE_SETUP.md).

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is open source and available under the MIT License.

## Support

For issues, questions, or contributions, please open an issue on GitHub.

