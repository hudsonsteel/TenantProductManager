# TenantProductManager

TenantProductManager is a multi-tenant web application designed to manage products and categories with support for user authentication and tenant-specific data. Built with ASP.NET Core 8 and utilizing JWT for authentication, this application provides a robust solution for managing and organizing product-related data across multiple tenants.

## Features

- **Multi-Tenant Support**: Manage multiple tenants with each having its own data isolated from others.
- **JWT Authentication**: Secure endpoints with JWT tokens.
- **CRUD Operations**: Create, Read, Update, and Delete operations for products and categories.
- **Inheritance of Data**: Child tenants inherit data from parent tenants but can also manage their own data.
- **Comprehensive API Documentation**: Detailed API documentation using Swagger.

## Database Schema

The following pictures provides an overview of the database schema used in the TenantProductManager application:

### Users:
![image](https://github.com/user-attachments/assets/afd12797-81b4-4ce9-9fcc-0dd3fbf698c4)

### Tenants:
![image](https://github.com/user-attachments/assets/a91837e7-b0f6-475d-b51f-df5535427bae)

### Categories:
![image](https://github.com/user-attachments/assets/053ba1e9-2cd8-48c9-8959-888baeba6388)

### Products:
![image](https://github.com/user-attachments/assets/f408baf9-1469-4c86-9e99-013cd343b139)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or compatible database
- [Docker](https://www.docker.com/products/docker-desktop) (optional, for running SQL Server and other services in containers)

### Setup

1. **Clone the Repository**

    ```bash
    git clone https://github.com/yourusername/TenantProductManager.git
    cd TenantProductManager
    ```

2. **Configure the Database**

   Update the connection string in `appsettings.json` to point to your SQL Server instance:

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_username;Password=your_password;"
    }
    ```

3. **Docker Setup**

   The project includes a `docker-compose.yml` file for setting up SQL Server and other services. This file also uses an `init.sql` script to initialize the database, create a default tenant, and set up an admin user. 

   To start the application and initialize the database using Docker, run:

    ```bash
    docker-compose up
    ```

   The `docker-compose.yml` file includes the following services:

   - **SQL Server**: The database server.
   - **Application**: The TenantProductManager application.

   The `init.sql` script, located in the root directory, is executed automatically when Docker starts up. This script:

   - Creates the database.
   - Creates a default tenant.
   - Sets up an admin user with initial credentials.

4. **Install Dependencies**

    Restore the NuGet packages:

    ```bash
    dotnet restore
    ```

5. **Run Migrations**

    Apply the database migrations:

    ```bash
    dotnet ef database update
    ```

6. **Run the Application**

    Start the application:

    ```bash
    dotnet run
    ```

    Alternatively, you can use Docker to run the application along with its dependencies. Ensure Docker is installed and then run:

    ```bash
    docker-compose up
    ```

### API Documentation

- Access the Swagger UI for detailed API documentation and testing: [http://localhost:5000/swagger](http://localhost:5000/swagger).

### API Endpoints

- **Authentication**
  - `POST /api/auth/login` - Authenticate a user and obtain a JWT token.
  - `POST /api/auth/register` - Create a new user.

- **Tenants**
  - `POST /api/tenants` - Create a new tenant.
  - `GET /api/tenants/{id}` - Retrieve tenant details by ID.
  - `PUT /api/tenants/{id}` - Update tenant details.
  - `DELETE /api/tenants/{id}` - Delete a tenant.

- **Categories**
  - `POST /api/categories` - Create a new category.
  - `GET /api/categories/{id}` - Retrieve category details by ID.
  - `PUT /api/categories/{id}` - Update category details.
  - `DELETE /api/categories/{id}` - Delete a category.

- **Products**
  - `POST /api/products` - Create a new product.
  - `GET /api/products/{id}` - Retrieve product details by ID.
  - `PUT /api/products/{id}` - Update product details.
  - `DELETE /api/products/{id}` - Delete a product.

