# Codex - Book Rental API

Codex is a RESTful API built with C\# and .NET 8, designed to power a comprehensive book rental system. The platform allows users to register their own bookstores, manage a detailed inventory of books, and define custom rental policies. Meanwhile, clients can seamlessly reserve and rent books through the system.

## Core Features

  - **Authentication & Authorization:** Secure access using **JSON Web Tokens (JWT)** with a role-based system (`Admin`, `Client`) to protect endpoints.
  - **Bookstore Management:** Users can create, update, and manage their own bookstore profiles and inventories.
  - **Centralized Book Catalog:** A complete CRUD for Books, Authors, and Categories, managed exclusively by administrators to ensure data integrity.
  - **Inventory Control:** Each bookstore manages its own stock (`BookItem`), including quantity and the physical condition of each book (e.g., New, Good, Acceptable).
  - **Customizable Rental Policies:** Bookstore owners can define their own rules for rental duration, pricing tiers, and late fees.
  - **Reservation & Rental Workflow:** A complete system allowing clients to reserve a book, convert the reservation into a rental upon pickup, and process returns.
  - **Resource Ownership Security:** Authorization policies ensure that only the owner of a resource (such as a bookstore or a reservation) can modify it.
  - **Global Exception Handling:** A middleware intercepts and formats all exceptions into a standardized JSON response, providing a consistent client experience.

-----

## Technology Stack

  - **Backend:** C\# 11, .NET 8, ASP.NET Core
  - **Database:** PostgreSQL
  - **ORM:** Entity Framework Core 8
  - **Authentication:** JSON Web Tokens (JWT)
  - **Architecture:** Layered Architecture (Controllers, Services, Repositories, Factories) with Dependency Injection.

-----

## Getting Started

### Prerequisites

  - [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
  - [PostgreSQL](https://www.postgresql.org/download/) (or a Docker instance)
  - A code editor such as [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/).

### 1\. Clone the Repository

```bash
git clone https://github.com/your-username/codex-backend.git
cd codex-backend
```

### 2\. Configure Environment Variables

This project uses `DotNetEnv` to load environment variables. It is recommended to create a `.env` file in the root of the project.

Create a `.env` file with the following structure:

```env
DB_HOST=localhost
DB_PORT=5432
DB_NAME=codex_db
DB_USER=postgres
DB_PASSWORD=your_secret_password

TokenSettings__Key=YOUR_VERY_LONG_AND_SECURE_SECRET_KEY
TokenSettings__Issuer=CodexAPI
TokenSettings__Audience=CodexUsers
```

Alternatively, you can configure these values directly in the `appsettings.json` file. The database connection string is built from the `DB_*` variables, and the JWT settings are read from the `TokenSettings` section.

### 3\. Apply Database Migrations

Run the following commands from the project's root directory to restore dependencies and create the database schema.

```bash
# Restore NuGet packages
dotnet restore

# Apply Entity Framework migrations
dotnet ef database update
```

### 4\. Run the Application

```bash
dotnet run
```

The API will be available at `https://localhost:7123` (the port may vary; check the terminal output). The Swagger documentation UI can be accessed at `https://localhost:7123/swagger`.

-----

## API Endpoint Documentation

The following is a summary of the main API endpoints.

**Access Levels:**

  - **Public:** No authentication required.
  - **Authenticated:** A valid JWT is required (any role).
  - **Admin:** A valid JWT with the "Admin" role is required.
  - **Resource Owner:** The authenticated user must be the owner of the resource being accessed.

### Auth Controller (`/api/auth`)

| Method | Endpoint    | Access  | Description                                |
| :----- | :---------- | :------ | :----------------------------------------- |
| `POST` | `/register` | Public  | Registers a new user with the "Client" role. |
| `POST` | `/login`    | Public  | Authenticates a user and returns a JWT.    |

### Bookstore Controller (`/api/bookstore`)

| Method   | Endpoint         | Access           | Description                                    |
| :------- | :--------------- | :--------------- | :--------------------------------------------- |
| `POST`   | `/create`        | Authenticated    | Creates a new bookstore for the logged-in user.  |
| `GET`    | `/get-all`       | Authenticated    | Retrieves a list of all available bookstores.    |
| `GET`    | `/my-bookstores` | Authenticated    | Lists the bookstores owned by the logged-in user. |
| `GET`    | `/by-id/{id}`    | Authenticated    | Fetches a bookstore by its unique ID.          |
| `PUT`    | `/update/{id}`   | Resource Owner   | Updates the details of a specific bookstore.     |
| `DELETE` | `/delete/{id}`   | Resource Owner   | Deactivates (soft delete) a bookstore.         |

### Book Controller (`/api/book`)

| Method   | Endpoint           | Access  | Description                                  |
| :------- | :----------------- | :------ | :------------------------------------------- |
| `POST`   | `/create`          | Admin   | Adds a new book to the global catalog.       |
| `GET`    | `/get-all`         | Public  | Retrieves all books from the catalog.        |
| `GET`    | `/by-id/{id}`      | Public  | Fetches a book by its unique ID.             |
| `GET`    | `/by-name/{title}` | Public  | Searches for books by title.                 |
| `PUT`    | `/update/{id}`     | Admin   | Updates a book's details.                    |
| `DELETE` | `/delete/{id}`     | Admin   | Deactivates (soft delete) a book.            |

### Reservation Controller (`/api/reservation`)

| Method   | Endpoint           | Access          | Description                                         |
| :------- | :----------------- | :-------------- | :-------------------------------------------------- |
| `POST`   | `/create`          | Authenticated   | Creates a book reservation for the logged-in user.  |
| `GET`    | `/my-reservations` | Authenticated   | Lists all reservations for the logged-in user.      |
| `GET`    | `/{id}`            | Resource Owner  | Fetches a specific reservation by its ID.           |
| `DELETE` | `/cancel/{id}`     | Resource Owner  | Cancels an existing reservation.                    |

*(Other controllers such as `Author`, `Category`, `BookItem`, `Rental`, and `StorePolicy` follow a similar CRUD structure with specific authorization rules.)*

-----

## Project Structure

The project is organized with a clear separation of concerns to promote maintainability and scalability.

  - `Application/`: Contains the core business logic.
      - `Controllers/`: Defines the API endpoints.
      - `Services/`: Implements the business logic and orchestration.
      - `Repositories/Interfaces/`: Defines the contracts for data access.
      - `Factories/`: Responsible for creating complex domain objects.
      - `Dtos/`: Data Transfer Objects for API requests and responses.
      - `Validators/`: Logic for validating incoming DTOs.
      - `Authorization/`: Handlers and Requirements for custom authorization policies.
  - `Infra/`: Contains infrastructure-level implementations.
      - `Repositories/`: Concrete repository implementations using Entity Framework Core.
  - `Database/`: Includes the `DbContext` and Entity Framework configurations.
  - `Models/`: Domain entities that map to database tables.
  - `Enums/`: Enumerations used throughout the application.
  - `Helpers/`: Utility classes (e.g., `PasswordHasher`).
