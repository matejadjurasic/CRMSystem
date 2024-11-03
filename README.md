# Project Management Dashboard

## Description

This project is a **Project Management Dashboard** with a **.NET Core API backend** and a **React frontend**. Built with a **clean architecture** approach on the backend, the application offers distinct dashboards for **users** and **admins**.

### Features

- **User Dashboard**:
  - Create new projects.
  - View a list of projects.
  - Modify current user data.
  - Modify user projects.
  - Graphical representation of the number of projects.

- **Admin Dashboard**:
  - View all projects across users.
  - View admin projects.
  - Modify admin projects.
  - Create clients.
  - Modify clients.
  - Automatically sends a password reset email when a new client account is created.

### Tech Stack

- **Backend**: .NET Core API

- **Frontend**: React
    - **TypeScript**: Provides type safety, making the code more robust and maintainable.

---

## Prerequisites

### Backend
- [.NET SDK](https://dotnet.microsoft.com/download) (version 6 or higher)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Frontend
- [Node.js](https://nodejs.org/) (version 16 or higher)
- [npm](https://www.npmjs.com/get-npm) or [yarn](https://yarnpkg.com/)

---

## Setup Instructions

### Backend

1. **Clone the repository**:
    ```bash
    git clone <repository-url>
    cd backend
    ```

2. **Configure Database**:
   Update the connection string in `appsettings.json` to match your SQL Server configuration.

3. **Run Database Migrations**:
    ```bash
    dotnet ef database update
    ```

4. **Run the Backend**:
    ```bash
    dotnet run
    ```

5. **Seed Initial Data** (Optional):
   If you've set up a seed mechanism, you can seed initial users, roles, and projects for testing purposes.

The backend API should now be running at `http://localhost:5000` (or a similar port as specified in your launch settings).

### Frontend

1. **Navigate to the frontend directory**:
    ```bash
    cd ../frontend
    ```

2. **Install Dependencies**:
    ```bash
    npm install
    ```

3. **Configure API URL**:
   Create a `.env` file in the frontend directory, if not already present, and set the API URL:
    REACT_APP_API_URL=http://localhost:5000
 

4. **Run the Frontend**:
 ```bash
 npm start
 ```

The frontend should now be accessible at `http://localhost:3000`.

---

## Usage

1. **Access the application** by navigating to `http://localhost:3000`.
2. **Sign in**:
- Use credentials set during the seed process (or set up manually in the database).

---

## Project Structure

### Backend - Clean Architecture

- **Domain**: Core business logic and entities.
- **Application**: Handles the application layer, including commands, queries, and validation.
- **Infrastructure**: External resources like database access, email sending, and repositories.
- **Presentation**: API layer, controllers, and request handling.

### Frontend

- **Redux**: State management to handle application state.
- **TypeScript**: Provides static type-checking for better code quality and maintainability.
- **Components**: Structured into functional, reusable components for modular UI development.

---

## Key Libraries

### Backend

- **MediatR**: For managing application commands and queries using CQRS.
- **FluentValidation**: Provides easy and fluent API validation.
- **EntityFramework Core**: For ORM and data access to SQL Server.
- **.NET Identity**: Built-in ASP.NET Core identity management.

### Frontend

- **Redux**: Manages the global application state.
- **React Router**: For navigation and routing within the application.
- **Recharts**: Provides charts.

---

## Security

- **JWT Tokens**: Used for authenticating users and protecting API endpoints.
- **Role-based Access**: The application enforces role-based authorization (e.g., Admin vs. User) to restrict access to certain features.

---

## Future Improvements

1. **Refactor Frontend Logic**

---

## License

This project is licensed under the MIT License. See the LICENSE file for details.
