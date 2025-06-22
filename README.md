#Advanced .NET 9 Web API Technical Assessment
Project Overview
This project implements a robust .NET 9 Web API for product management with full CRUD operations. It demonstrates proper layering architecture, dependency injection, RESTful principles, unit testing, and best practices for modern API development.

Technology Stack
	Framework: .NET 9

	Database: SQL Server (via Entity Framework Core)

	Testing: xUnit + Moq

	API Documentation: OpenAPI (Swagger)

	Architecture: Layered (API + Core + Infrastructure)

Features
Core Functionality
	Product CRUD Operations:

		Create product (POST /api/products)

		Get all products (GET /api/products)

		Get single product (GET /api/products/{id})

		Update product (PUT /api/products/{id})

		Delete product (DELETE /api/products/{id})

		Model Validation (Data Annotations)

		Global Exception Handling Middleware

		Asynchronous Programming Model

		Dependency Injection Implementation

Advanced Features
	DTO Pattern: Separation of API models from domain entities

	EF Core Persistence: SQL Server database support

	Global Exception Handling: Standardized error response format

	Unit Testing: Full coverage of controllers and services

	OpenAPI Documentation: Integrated Swagger UI

	Middleware Implementation: Custom exception handler

Project Structure

	TechnicalTest/
	├── src/
	│   ├── Api/                 # Web API Project (MyCompany.Test.Api)
	│   ├── Core/                # Domain Models & Interfaces (MyCompany.Test.Core)
	│   └── Infrastructure/      # Service Implementation & Persistence (MyCompany.Test.Infrastructure)
	├── tests/
	│   ├── Api.Tests/           # Controller Unit Tests
	│   └── Core.Tests/          # Service Layer Unit Tests
	└── TechnicalTest.sln        # Solution File


Build & Run
Prerequisites
	.NET 9 SDK

	SQL Server (local or remote instance)

Database Configuration
	1.Add connection string to appsettings.json:

	{
	  "ConnectionStrings": {
		"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProductDB;Trusted_Connection=True;"
	  }
	}

Execution Steps
	1.Restore dependencies:

		bash
		dotnet restore

	2.Apply database migrations:

		bash
		dotnet ef database update --project src/Infrastructure

	3.Run the API:

		bash
		dotnet run --project src/Api
	
	4.Access endpoints:

		Swagger UI: https://localhost:5000/swagger

		API Endpoint: https://localhost:5000/api/products

Running Tests
	Run all tests
		bash
		dotnet test

	Run specific test projects
		bash
		# API Tests
		dotnet test tests/Api.Tests

		# Core Service Tests
		dotnet test tests/Core.Tests

	Generate coverage report
		bash
		dotnet test --collect:"XPlat Code Coverage"

Implemented Features

Feature	                     Status	               Description
EF Core Persistence	         ✅ Implemented	       SQL Server database
DTO Pattern	                 ✅ Implemented	       ProductDto and ProductModel
Global Exception Handling	 ✅ Implemented	       Custom middleware
Model Validation	         ✅ Implemented	       Data Annotations
Unit Testing	             ✅ Implemented	       Full controller/service coverage
OpenAPI Documentation	     ✅ Implemented	       Swagger UI integration
JWT Authentication	         ❌ Not Implemented	
Health Checks	             ❌ Not Implemented	
CI/CD Pipeline	             ❌ Not Implemented	


Design Decisions

Architecture
	Layered Architecture:

		API Layer: Handles HTTP requests/responses

		Core Layer: Contains domain models and interfaces

		Infrastructure Layer: Implements services and persistence

	Dependency Injection: All services registered via DI container for testability

Persistence

	SQL Server: Chosen for enterprise-grade reliability

	Entity Framework Core: Simplifies data access

	Code-First Approach: Database schema managed via migrations

Exception Handling
	Custom Exceptions:

		NotFoundException: Resource not found (404)

		BadRequestException: Client request errors (400)

	Global Exception Middleware: Standardized error responses for unhandled exceptions

API Design
	RESTful Principles: Adheres to REST conventions

	HTTP Status Codes:

		200 OK: Successful GET requests

		201 Created: Resource creation success

		204 No Content: Successful operations with no body

		400 Bad Request: Invalid client input

		404 Not Found: Resource not found

	DTO Pattern:

		ProductModel: API input model

		ProductDto: API output model

Testing Strategy
	Unit Testing:

		Controllers: Mocked service layer using Moq

		Services: In-memory database for realistic testing

	Test Coverage:

		Controllers: 100% endpoint coverage

		Services: >85% business logic coverage

Known Issues & Improvements
	Authentication Missing: No auth mechanism implemented

		Improvement: Integrate JWT Bearer authentication

	No Integration Tests: Only unit tests currently exist

		Improvement: Add end-to-end tests using TestServer

	Performance Optimization: No caching implemented

		Improvement: Add Redis caching layer

	Pagination Support: GET all products lacks pagination

		Improvement: Implement pagination parameters and headers

Contribution Guide
	1.Create a feature branch:

		bash
		git checkout -b feature/your-feature-name

	2.Commit changes:

		bash
		git commit -m "feat: add new feature"

	3.Push to remote:

		bash
		git push origin feature/your-feature-name

	4.Create Pull Request with detailed description

License
This project is licensed under the MIT License.