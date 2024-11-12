# MicroserviceTester

![MicroserviceTester Logo](https://via.placeholder.com/150)

**MicroserviceTester** is a comprehensive .NET 8.0-based project designed to simulate and test the interactions between multiple microservices. It encompasses User, Product, and Order services, each with their own models, controllers, and services. The project leverages a variety of testing methodologies, including Unit Testing, Integration Testing, Behavior-Driven Development (BDD) Testing, Contract Testing, and Performance Benchmarking, ensuring robust and reliable microservice interactions. Additionally, the project employs code coverage tools to guarantee thorough testing coverage.

## Table of Contents

- [Project Overview](#project-overview)
- [Technologies Used](#technologies-used)
- [Microservices Architecture](#microservices-architecture)
  - [User Service](#user-service)
  - [Product Service](#product-service)
  - [Order Service](#order-service)
- [Setup and Installation](#setup-and-installation)
  - [Prerequisites](#prerequisites)
  - [Installation Steps](#installation-steps)
- [Running the Application](#running-the-application)
  - [Using Swagger UI](#using-swagger-ui)
- [Testing](#testing)
  - [Unit Tests](#unit-tests)
  - [Integration Tests](#integration-tests)
  - [BDD Tests](#bdd-tests)
  - [Contract Tests](#contract-tests)
  - [Concurrency Tests](#concurrency-tests)
  - [Model Validation Tests](#model-validation-tests)
  - [Performance Tests](#performance-tests)
  - [Code Coverage](#code-coverage)
- [Licenses](#licenses)
- [Contributing](#contributing)
- [Contact](#contact)

## Project Overview

MicroserviceTester is engineered to mimic the functionality of a microservices-based application, encompassing three primary services:

1. **User Service**: Manages user data and operations.
2. **Product Service**: Handles product information and related operations.
3. **Order Service**: Facilitates order creation and management, linking users and products.

The project emphasizes thorough testing to ensure each service operates correctly both in isolation and in concert with others. By implementing various testing strategies, MicroserviceTester ensures reliability, scalability, and maintainability.

## Technologies Used

- **.NET 8.0**: Core framework for building the microservices.
- **ASP.NET Core Web API**: For creating RESTful APIs.
- **xUnit**: Testing framework for Unit, Integration, and Model Validation tests.
- **Moq**: Mocking framework for isolating dependencies in Unit Tests.
- **FluentAssertions**: For more readable and maintainable assertions.
- **SpecFlow**: BDD framework for defining and running BDD tests.
- **WireMock.Net**: Mock server for Contract Testing.
- **BenchmarkDotNet**: For Performance Benchmarking.
- **Coverlet**: Code coverage tool integrated with the testing framework.
- **Swagger (Swashbuckle.AspNetCore)**: API documentation and testing UI.
- **PactNet**: For consumer-driven contract testing.
- **Microsoft.AspNetCore.Mvc.Testing**: Facilitates integration testing of ASP.NET Core applications.

## Microservices Architecture

MicroserviceTester comprises three distinct services, each responsible for specific domain functionalities. These services interact seamlessly to simulate a real-world microservices ecosystem.

### User Service

- **Models**: Defines the `User` entity with validation attributes ensuring data integrity.
- **Services**: Implements `IUserService` using a thread-safe `ConcurrentDictionary` to manage users.
- **Controllers**: Exposes RESTful endpoints for creating, retrieving, and deleting users with proper exception handling and logging.

### Product Service

- **Models**: Defines the `Product` entity.
- **Services**: Implements `IProductService` using a `List<Product>` for managing products with thread safety considerations.
- **Controllers**: Provides endpoints to create and retrieve products, ensuring data consistency and conflict handling.

### Order Service

- **Models**: Defines the `Order` entity linking `User` and `Product`.
- **Services**: Implements `IOrderService` with dependencies on `IUserService` and `IProductService` to ensure orders are only created for existing users and products.
- **Controllers**: Offers endpoints for order creation and retrieval, enforcing business rules and ensuring data integrity.

## Setup and Installation

### Prerequisites

Ensure you have the following installed on your machine:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) with C# extensions
- [Git](https://git-scm.com/downloads)

### Installation Steps

1. **Clone the Repository**

   `git clone https://github.com/yourusername/MicroserviceTester.git`
cd MicroserviceTester

3. **Restore Dependencies**
Navigate to the project directory and restore the NuGet packages:

`dotnet restore`

3. **Build the Project**

`dotnet build`
