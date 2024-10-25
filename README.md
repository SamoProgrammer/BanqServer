![Banq Server](https://github.com/SamoProgrammer/BanqServer/blob/main/BanqServer.webp)
<em>image generated with AI</em>

# BanqServer

BanqServer is a backend API designed for managing question banks within an educational environment. It provides a scalable and secure way for schools to manage question storage, retrieval, and organization. This API is built with ASP.NET Core, following clean architecture principles, and includes support for Docker deployment.

## Table of Contents

- [Features](#features)
- [Project Structure](#project-structure)
- [Setup and Installation](#setup-and-installation)
- [Configuration](#configuration)
- [API Endpoints](#api-endpoints)
- [Authentication](#authentication)
- [Docker Support](#docker-support)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Question Management**: CRUD operations for creating, retrieving, updating, and deleting questions.
- **User Authentication**: Secured endpoints for user access and role management.
- **Database Migrations**: Integrated migrations for database setup and updates.
- **Docker Support**: Easily deployable via Docker for consistent production and development environments.

## Project Structure

The project is structured as follows:

```plaintext
BanqServer/
├── Authentication/           # Authentication logic and user management
├── Controllers/              # API endpoints
├── DTOs/                     # Data Transfer Objects for data shaping
├── Database/                 # Database setup and configuration
├── Extensions/               # Extension methods
├── Migrations/               # Database migrations
├── Utilities/                # Helper functions
├── ViewModels/               # Models for API responses
├── appsettings.json          # Application configuration
├── Dockerfile                # Docker configuration
├── Banq.csproj                # Project configuration and dependencies
└── Program.cs                # Main entry point of the application
```

## Setup and Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/BanqServer.git
   cd BanqServer
   ```

2. **Restore Dependencies**:
   Use the following command to install the required packages:
   ```bash
   dotnet restore
   ```

3. **Apply Database Migrations**:
   Ensure the database is set up by applying migrations:
   ```bash
   dotnet ef database update
   ```

4. **Run the Application**:
   Start the server with:
   ```bash
   dotnet run
   ```

The server will run on `http://localhost:5000` by default.

## Configuration

BanqServer uses `appsettings.json` for configuration management. Adjust the database connection strings and other settings as necessary for your environment.

- **`appsettings.json`**: Main configuration file.
- **`appsettings.Development.json`**: Configuration overrides for development.

## API Endpoints

The following are key API endpoints for managing questions:

| Endpoint                | Method | Description               |
|-------------------------|--------|---------------------------|
| `/api/questions`        | GET    | Retrieve all questions    |
| `/api/questions/{id}`   | GET    | Retrieve a question by ID |
| `/api/questions`        | POST   | Create a new question     |
| `/api/questions/{id}`   | PUT    | Update a question         |
| `/api/questions/{id}`   | DELETE | Delete a question         |

Detailed API documentation is available via Swagger at `http://localhost:5000/swagger`.

## Authentication

BanqServer includes authentication and role-based access to restrict access to certain endpoints. Authentication is implemented in the `Authentication` folder and configured in `appsettings.json`.

## Docker Support

To build and run BanqServer using Docker:

1. **Build the Docker Image**:
   ```bash
   docker build -t banqserver .
   ```

2. **Run the Docker Container**:
   ```bash
   docker run -p 5000:5000 banqserver
   ```
