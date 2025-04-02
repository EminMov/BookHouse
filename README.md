The BookHouse project is an online bookstore implemented as a web API using ASP.NET Core. The source code is available on GitHub.

Technologies Used:
C# / .NET 7 – main programming language and framework.

ASP.NET Core Web API – implementation of a RESTful API for client interaction.

Entity Framework Core – ORM for database interactions.

SQL Server – database for storing information about books, users, and orders.

AutoMapper – for mapping DTOs to domain models and vice versa.

FluentValidation – for input data validation.

Dependency Injection (DI) – for flexible dependency management.

Repository Pattern – abstraction for data access, improving maintainability and testability.

Project Architecture:
The project follows a multi-layered architecture:

BookHouse.Core – domain logic and business rules.

BookHouse.Infrastructure – database interactions and repositories.

BookHouseAPI (Presentation Layer) – REST API handling requests and responses.

Project Features:
Book catalog management (create, update, delete, search).

User registration and authentication.

Order placement and management.

Input data validation.

This project demonstrates the application of modern development principles, including SOLID, Separation of Concerns (SoC), and Clean Architecture.
