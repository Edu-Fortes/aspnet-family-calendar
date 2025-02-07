# Family Calendar Planner - Backend

This project is a web application built with ASP.NET Core and Entity Framework Core. It serves as a portfolio piece to demonstrate my skills in building and deploying web applications. The application includes user and event management functionalities.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
  - [Running the Application](#running-the-application)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- User management
- Event management
- CORS support for local development
- HTTPS redirection

## Technologies

- C# 13.0
- .NET 9
- Entity Framework Core
- PostgreSQL

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Installation

1. Clone the repository:

```bash
git clone https://github.com/Edu-Fortes/aspnet-family-calendar
cd aspnet-family-calendar
```

2. Open the solution in Visual Studio 2022.

### Database Setup

1. Create a PostgreSQL database and update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=your-database-name;Username=your-username;Password=your-password"
}
```
2. Apply migrations to set up the database schema:

```bash
dotnet ef database update
```

### Running the Application

1. Build and run the application:
    
```bash
dotnet run
```

2. The application will be available at `https://localhost:5001`.

## Usage

- The application supports CORS for `http://localhost:4200`, allowing you to interact with it from a frontend application running on that origin.
- User and event management endpoints are available as defined in `UsersController` and `EventsController`.
- - To run the frontend application, clone the [frontend repository](https://github.com/Edu-Fortes/angular-family-calendar) and follow its setup instructions. Ensure it is configured to interact with the backend running at `https://localhost:5001`.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.