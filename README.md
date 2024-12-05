
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
3. **Docker**: Ensure Docker is installed for containerized MySQL.

---

### Configuring `appsettings.json`

The API uses the following keys in `appsettings.json` or `appsettings.Development.json` to configure its behavior:

#### **`AppSettings` Section**
```json
"AppSettings": {
  "UseDatabase": true,
  "UserJsonFilePath": "..\\..\\..\\..\\Second.Cojali.Infrastructure\\Data\\users.json",
  "ConnectionString": "Server=localhost;Database=SecondCojaliDb;User=root;Password=yourpassword;"
}
```

1. **`UseDatabase`**:
   - Determines whether the application should use a MySQL database (`true`) or read/write user data from a JSON file (`false`).
   - Default: `true`. In case the setting is not present the value will be `false`.

2. **`UserJsonFilePath`**:
   - Specifies the relative path to the JSON file used when `UseDatabase` is `false`.
   - The relative path is resolved using the application’s base directory:
     ```csharp
     var absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));
     ```
   - Example paths and usage are provided in the earlier section.

3. **`ConnectionString`**:
   - Defines the connection string for the MySQL database when `UseDatabase` is `true`.
   - Example:
     ```json
     "ConnectionString": "Server=localhost;Database=SecondCojaliDb;User=root;Password=yourpassword;"
     ```
   - Update the values for `Server`, `Database`, `User`, and `Password` based on your MySQL setup.

---

### Running with Docker Compose

To use the API with a MySQL database hosted in a Docker container, follow these steps:

#### **1. Docker Compose Setup**
Ensure you have a `docker-compose.yml` file in your project with the following content:

```yaml
version: '3.8'

services:
  mysql:
    image: mysql:8.0
    container_name: mysql_container
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: yourpassword
      MYSQL_DATABASE: SecondCojaliDb
    ports:
      - "3306:3306"
    volumes:
      - db_data:/var/lib/mysql
      - ./scripts:/docker-entrypoint-initdb.d

volumes:
  db_data:
```

- **MySQL Container**:
  - `MYSQL_ROOT_PASSWORD`: Set your desired root password.
  - `MYSQL_DATABASE`: Creates the `SecondCojaliDb` database automatically.
- **Volumes**:
  - `db_data`: Ensures the database data persists even after the container is stopped.
  - `./scripts`: Place initialization scripts here (e.g., `create_tables.sql`).

#### **2. Initialize the MySQL Container**
Run the following command to start the container:

```bash
docker-compose up -d
```

Verify the container is running:

```bash
docker ps
```

#### **3. Update the Connection String**
Update the `ConnectionString` in `appsettings.json` to point to the MySQL container:

```json
"ConnectionString": "Server=localhost;Database=SecondCojaliDb;User=root;Password=yourpassword;"
```

#### **4. Run the API**
Start the API project using the following command:

```bash
dotnet run --project Second.Cojali.Api
```

#### **5. Verify the Setup**
- Ensure the API connects to the MySQL container by testing the endpoints.
- Check the MySQL container logs to confirm connections.

---

## Usage

### Endpoints
- **GET /users**: Retrieves a list of users from the JSON file or MySQL database.
- **POST /users**: Adds a new user to the JSON file or MySQL database and simulates sending an email.
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