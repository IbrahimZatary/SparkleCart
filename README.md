# SparkleCart 
## E-Commerce Web API (.NET Core 10)
## Overview
This project is a RESTful Web API built using .NET Core 9 for a simple e-commerce system.  
It provides core backend functionalities including authentication, product and category management, order handling, and shopping cart operations.

The API follows clean architecture principles and REST conventions, making it scalable and easy to extend.

---

## Features

### Authentication & Users
- User Registration (Sign-Up)
- User Login (Sign-In)
- Secure password hashing
- JWT-based authentication
- Protected endpoints using JWT authorization

### Products Module
- Get all products  
- Get product by ID  
- Create product  
- Update product  
- Delete product  

### Categories Module
- Get all categories  
- Get category by ID  
- Create category  
- Update category  
- Delete category  

### Orders Module
- Get all orders  
- Get order by ID  
- Update order  

### Cart Module
- Get current user's cart  
- Add product to cart  
- Update product quantity  
- Checkout cart  

---

## Tech Stack
- .NET Core 9 Web API
- Entity Framework Core (Code First)
- SQL Server
- JWT Authentication
- Swagger

---

## Architecture & Design
- RESTful API design
- Separation of concerns (Controllers, Services, Data Layer)
- Entity Framework Core with Code First approach
- Database migrations
- Global error handling middleware
- Logging support

---

## Database Configuration

Update connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ECommerceDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
