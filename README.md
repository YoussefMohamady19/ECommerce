#  ECommerce API

A clean and modular **E-Commerce RESTful API** built with **ASP.NET Core 8** and **Entity Framework Core** using layered architecture principles.  
Implements **Repository & Unit of Work Patterns**, **FluentValidation**, **AutoMapper**, and **xUnit Tests**.

---

## Features

-  **Clean Architecture** — separated into Domain, Application, Infrastructure, and API layers.  
-  **Entity Framework Core** — supports InMemory and SQL Server databases.  
-  **Repository + Unit of Work Pattern** — clean data access abstraction.  
-  **FluentValidation** — for model validation.  
-  **AutoMapper** — to map between entities and DTOs.  
-  **xUnit Integration Tests** — testing main API endpoints.  
-  **Swagger UI** — built-in documentation and testing interface.

ECommerce/
│
├── ECommerce.Domain/ → Entities and core business models
├── ECommerce.Application/ → DTOs, Services, Validators, Mapping Profiles
├── ECommerce.Infrastructure/ → DbContext, Repositories, UnitOfWork
├── ECommerce.API/ → API Controllers and Startup configuration
└── ECommerce.Tests/ → Integration and Unit tests


---

##  Database Setup

The API uses **Entity Framework Core**.

###  To Use SQL Server:
1. Update connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }

Add-Migration InitialCreate -Project ECommerce.Infrastructure -StartupProject ECommerce.API
Update-Database

## Example Entities

Customer

Id, Name, Email, Phone

Product

Id, Name, Price, Stock

Order

Id, CustomerId, OrderDate, Status, TotalPrice

Includes a list of OrderProducts with product quantities.

📬 Sample API Endpoints
Method	Endpoint	Description
POST	/api/customers	Create new customer
GET	/api/customers	Retrieve all customers
GET	/api/customers/{id}	Retrieve specific customer

POST	/api/orders	Create order for a customer
GET	/api/orders/{id}	Retrieve order details
PUT	/api/orders/{id}/delivered	Mark order as delivered

## Unit & Integration Testing

Tests are built using xUnit and WebApplicationFactory for full API testing.

Example test files:

CustomersControllerTests.cs

OrdersControllerTests.cs

## Tools & Technologies

ASP.NET Core 8

Entity Framework Core

AutoMapper

FluentValidation

Swagger / OpenAPI

xUnit

Visual Studio 2022

Postman (for API testing)

## Database Schema
You Can open file Database Schema.docx

## Postman Collection

You can import the ready-made Postman collection for testing the API:
 ECommerce_API.postman_collection.json

 Author

## Youssef Mohamady
- yousef.uouo9@gmail.com

- Full Stack .NET Developer
