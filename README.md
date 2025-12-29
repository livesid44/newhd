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
- SQL Server LocalDB (or SQL Server instance)
- Visual Studio 2022 / Visual Studio Code / Rider (optional)

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

The application uses SQL Server LocalDB by default. The connection string in `VisitManagement/appsettings.json` is already configured:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Database=VisitManagementDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Command Timeout=0"
  }
}
```

If you want to use a different SQL Server instance, update this connection string accordingly.

### 4. Create Database and Run Migrations

The migrations are already created. Simply apply them to create the database:

```bash
cd VisitManagement
dotnet ef database update
```

This will create the database with all tables and seed data (2 sample visits and 3 sample users).

### 5. Run the Application

```bash
dotnet run
```

The application will start and be accessible at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

### 6. Register and Login

On first run, you'll be redirected to the login page. Click "Register here" to create a new account:
- Enter your full name, email, and password
- Password must be at least 8 characters with uppercase, lowercase, digit, and special character
- After registration, you'll be automatically logged in

## Authentication

The application uses ASP.NET Core Identity for secure authentication:

- **Login**: Access at `/Account/Login` or click "Login" in the navigation
- **Register**: Access at `/Account/Register` or click "Register" in the navigation
- **Logout**: Click on your username dropdown and select "Logout"
- **Protected Routes**: Visit and User management pages require authentication
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
2. **Click "Register here"** to create your first account
3. **Fill in the registration form**:
   - Full Name
   - Email (must be unique)
   - Password (minimum 8 characters, must include uppercase, lowercase, digit, and special character)
   - Confirm Password
4. **After registration**, you'll be automatically logged in and redirected to the Visits page

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
- **SQL Server**: Database engine
- **Bootstrap 5**: Responsive UI framework
- **Bootstrap Icons**: Icon library
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

### Database Errors

If you encounter the error `Invalid object name 'Visits'`, it means the database hasn't been created yet. Follow these steps:

1. **Install EF Core Tools** (if not already installed):
   ```bash
   dotnet tool install --global dotnet-ef --version 8.0.0
   ```

2. **Navigate to the project folder**:
   ```bash
   cd VisitManagement
   ```

3. **Apply the migration to create the database**:
   ```bash
   dotnet ef database update
   ```

   This will create the database with all tables and seed data.

### Decimal Precision Warning

The warning about `TcvMnUsd` decimal precision has been resolved in the latest version. The property is now configured with `decimal(18,2)` precision in the database schema. If you see this warning:

1. Ensure you're using the latest version of the code
2. Remove old migrations if any: `dotnet ef migrations remove`
3. Create a fresh migration: `dotnet ef migrations add InitialCreate`
4. Update the database: `dotnet ef database update`

### Connection String Issues

The application uses SQL Server LocalDB with the following connection string:
```
Data Source=(localdb)\MSSQLLocalDB;Database=VisitManagementDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Command Timeout=0
```

If you encounter connection issues:
- Ensure SQL Server LocalDB is installed
- Verify the instance name matches `(localdb)\MSSQLLocalDB`
- Check that Windows Authentication is enabled
- Try running the application as Administrator

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is open source and available under the MIT License.

## Support

For issues, questions, or contributions, please open an issue on GitHub.

