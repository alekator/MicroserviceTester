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

3. **Restore Dependencies**
   Navigate to the project directory and restore the NuGet packages:

   `dotnet restore`

3. **Build the Project**

   `dotnet build`
   
### Running the Application
To run the microservices application:

1. **Navigate to the Project Directory**
 `cd MicroserviceTester`
2. **Run the Application**
 `dotnet run`
The application will start and listen on the default ports. You should see console output indicating that the application is running.

### Using Swagger UI
Swagger UI is integrated into the application for easy testing and exploration of the APIs.

1. **Access Swagger UI**

   Open your browser and navigate to: `http://localhost:<PORT>/swagger`
   Replace <PORT> with the port number displayed in the console output when the application starts.

2. **Interact with APIs**

   Use the interactive Swagger interface to test various endpoints for Users, Products, and Orders.

### Testing
MicroserviceTester incorporates a diverse suite of tests to ensure each microservice functions correctly both individually and collectively. The testing strategy encompasses Unit Tests, Integration Tests, BDD Tests, Contract Tests, Concurrency Tests, Model Validation Tests, and Performance Tests.

## Unit Tests
- Purpose: Validate the functionality of individual components in isolation.
- Tools Used: xUnit, Moq, FluentAssertions.
- Coverage:
 - UserServiceTests: Tests for adding and retrieving users.
 - ProductServiceTests: Tests for adding and retrieving products.
 - OrderServiceTests: Tests for adding and retrieving orders, ensuring dependencies on UserService and ProductService are respected.
## Integration Tests
Purpose: Test the interactions between different microservices and ensure they work together as expected.
Tools Used: xUnit, Microsoft.AspNetCore.Mvc.Testing, FluentAssertions.
Coverage:
ApplicationEndToEndTests: Simulates full workflows including user, product, and order creation.
ExtendedApplicationEndToEndTests: Additional scenarios to verify the robustness of service interactions.
## BDD Tests
Purpose: Define and execute behavior-driven scenarios to validate the system's behavior from an end-user perspective.
Tools Used: SpecFlow, xUnit, FluentAssertions.
Coverage:
UsersSteps: Steps for creating users, products, and orders, and verifying the persistence of orders post user deletion.
## Contract Tests
Purpose: Ensure that microservices adhere to agreed-upon contracts, facilitating reliable communication between services.
Tools Used: WireMock.Net, xUnit, FluentAssertions.
Coverage:
OrderServiceContractTests: Validates order creation contracts.
ProductServiceContractTests: Validates product creation contracts.
UserServiceContractTests: Confirms that UserService implements the necessary interfaces.
## Concurrency Tests
Purpose: Assess how the system handles multiple simultaneous operations, ensuring thread safety and data integrity.
Tools Used: xUnit, FluentAssertions, System.Threading.
Coverage:
UserServiceConcurrencyTests: Tests concurrent user creation, ensuring only one user is created and others receive conflict responses.
## Model Validation Tests
Purpose: Verify that data models enforce validation rules, preventing invalid data from being processed.
Tools Used: xUnit, FluentAssertions.
Coverage:
UserModelValidationTests: Tests scenarios like missing username and duplicate user IDs, ensuring appropriate HTTP responses.
## Performance Tests
Purpose: Measure the performance and efficiency of the services under various conditions.
Tools Used: BenchmarkDotNet.
Coverage:
UserServiceBenchmark: Benchmarks the GetUserById and DeleteUser methods to assess their performance metrics.
## Code Coverage
Purpose: Ensure that the test suite adequately covers the codebase, identifying untested areas.
Tools Used: Coverlet integrated with xUnit.
Configuration:
Code coverage is configured in the .csproj file with GenerateCoverageFile and CoverletOutputFormat set to opencover.
Generating Coverage Report:
bash
Копировать код
dotnet test /p:CollectCoverage=true
The coverage report will be generated in the specified format, allowing for analysis of test coverage.
