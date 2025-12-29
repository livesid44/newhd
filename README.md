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

The application uses SQL Server LocalDB by default. If you want to use a different SQL Server instance, update the connection string in `VisitManagement/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VisitManagementDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 4. Create Database and Run Migrations

```bash
cd VisitManagement
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

The application will start and be accessible at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

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
│   │   ├── VisitsController.cs
│   │   └── UsersController.cs
│   ├── Data/                     # Database context
│   │   └── ApplicationDbContext.cs
│   ├── Models/                   # Data models
│   │   ├── Visit.cs
│   │   └── User.cs
│   ├── Views/                    # Razor views
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

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is open source and available under the MIT License.

## Support

For issues, questions, or contributions, please open an issue on GitHub.

