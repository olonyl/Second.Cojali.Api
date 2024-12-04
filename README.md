
# Second.Cojali.Api

## Overview

This repository contains the **Second.Cojali.Api**, a backend exercise designed to demonstrate proficiency in C#, .NET Core, and clean architectural principles. This project implements a REST API for managing user data, simulating real-world backend development scenarios.

### Repository URL
```
git@github.com:olonyl/Second.Cojali.Api.git
```

### Context
This project was developed as part of a backend exercise for a job application. The exercise requirements include:

- **Technologies**: C#
- **Tasks**:
  1. Create a REST API with the following endpoints:
     - **GET**: Retrieve a list of users from a JSON file.
     - **POST**: Add a new user to the JSON file and simulate sending an email to the created user (no actual email is sent).
     - **PUT**: Update the data of an existing user.
  2. Provide a `README.md` file with instructions to execute the application.

### Evaluation Criteria
- Folder and file structure.
- Code readability.
- Use of **Domain-Driven Design (DDD)**.
- Implementation of **Hexagonal Architecture**.
- Adherence to **SOLID Principles**.

---

## Folder Structure

- **`.vs`**: Visual Studio project metadata.
- **`Second.Cojali.Api`**: Main API implementation containing controllers, middleware, and configurations.
- **`Second.Cojali.Api.Contracts`**: Interface definitions, DTOs, and contracts for communication between layers.
- **`Second.Cojali.Api.IoC`**: Dependency injection and service registration.
- **`Second.Cojali.Api.sln`**: Solution file for Visual Studio.
- **`Second.Cojali.Application`**: Contains application-level services and business logic.
- **`Second.Cojali.Domain`**: Core entities, value objects, and business rules.
- **`Second.Cojali.Infrastructure`**: Data access, persistence logic, and integrations.

---

## Setup Instructions

### Prerequisites
1. **.NET SDK**: Download the latest .NET SDK from [Microsoft](https://dotnet.microsoft.com/).
2. **Visual Studio**: Use Visual Studio or another IDE that supports .NET Core.
3. **JSON Data File**: Ensure the JSON file for user data is available in the correct location (typically in the API project folder).

### Steps to Run
1. Clone the repository:
   ```bash
   git clone git@github.com:olonyl/Second.Cojali.Api.git
   ```
2. Open the solution in Visual Studio:
   ```bash
   cd Second.Cojali.Api
   start Second.Cojali.Api.sln
   ```
3. Restore NuGet dependencies:
   ```bash
   dotnet restore
   ```
4. Update the `appsettings.json` file with any required configurations.
5. Run the project:
   ```bash
   dotnet run --project Second.Cojali.Api
   ```

---

## Usage

### Endpoints
- **GET /users**: Retrieves a list of users from the JSON file.
- **POST /users**: Adds a new user to the JSON file and simulates sending an email.
- **PUT /users/{id}**: Updates the information of an existing user.

### API Testing
Use [Postman](https://www.postman.com/) or Swagger (if implemented) to test the API endpoints. If Swagger is enabled, documentation can be accessed at:
```
http://localhost:[port]/swagger
```

---

## Contribution Guidelines

1. **Branch Naming**: Use feature-specific branches (e.g., `feature/add-user-endpoint`).
2. **Code Standards**: Adhere to clean code principles and SOLID practices.
3. **Tests**: Write unit tests for any new features or bug fixes.
4. **Pull Requests**: Provide a clear and descriptive summary of changes in PRs.

---

## License
This project is licensed under the MIT License.

## Acknowledgments
- Developed as part of a technical exercise.
- Inspired by best practices in backend architecture and design patterns.