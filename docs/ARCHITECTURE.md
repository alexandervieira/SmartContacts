# SmartContacts Architecture

## Overview

SmartContacts follows a **Hexagonal Architecture** (Ports & Adapters) combined with **Domain-Driven Design (DDD)** principles to create a maintainable, testable, and scalable contact management application.

## Architecture Layers

### 1. Domain Layer (`AVS.Contacts.Domain`)
- **Entities**: Core business objects (Contact)
- **Value Objects**: Immutable objects (Name, Address, PhoneNumber)
- **Repositories**: Interfaces for data access
- **Common**: Base classes and shared domain logic

**Key Principles:**
- No external dependencies
- Pure business logic
- Rich domain model

### 2. Application Layer (`AVS.Contacts.Application`)
- **Commands**: Write operations using MediatR
- **Queries**: Read operations using MediatR
- **Handlers**: Command and query handlers
- **Mappings**: AutoMapper profiles
- **Services**: Application-specific services

**Key Principles:**
- Orchestrates domain objects
- Implements use cases
- Dependency inversion

### 3. Infrastructure Layer (`AVS.Contacts.Infrastructure`)
- **MongoDB**: Data persistence implementation
- **Authentication**: Azure B2C integration
- **Configuration**: Settings and dependency injection

**Key Principles:**
- Implements domain interfaces
- External system integrations
- Framework-specific code

### 4. Anti-Corruption Layer (`AVS.Contacts.ACL`)
- **Azure Speech Services**: Voice recognition integration
- **External API adapters**: Third-party service wrappers

**Key Principles:**
- Protects domain from external changes
- Translates external models to domain models
- Isolates external dependencies

### 5. Contracts Layer (`AVS.Contacts.Contracts`)
- **DTOs**: Data transfer objects
- **Service Interfaces**: Shared contracts

**Key Principles:**
- Shared between layers
- No business logic
- Simple data structures

### 6. Presentation Layer (`AVS.Contacts.Mobile`)
- **MAUI Application**: Cross-platform UI
- **ViewModels**: MVVM pattern implementation
- **Views**: XAML pages and controls

## Design Patterns Used

### 1. Repository Pattern
```csharp
public interface IRepository<TEntity, TId>
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken ct = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
    // ... other methods
}
```

### 2. CQRS with MediatR
```csharp
// Command
public record CreateContactCommand(CreateContactDto Contact) : IRequest<ContactDto>;

// Query
public record GetContactsQuery : IRequest<IEnumerable<ContactDto>>;
```

### 3. Value Objects
```csharp
public class Name
{
    public string FirstName { get; }
    public string LastName { get; }
    
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
```

### 4. Dependency Injection
Each layer has its own `DependencyInjection` class for service registration.

## Technology Stack

### Core Framework
- **.NET 10**: Latest .NET version
- **.NET MAUI**: Cross-platform UI framework

### Data & Persistence
- **MongoDB**: Document database
- **MongoDB.Driver**: Official .NET driver

### Communication & Messaging
- **MediatR**: In-process messaging
- **AutoMapper**: Object-to-object mapping

### Authentication
- **Azure B2C**: Identity and access management
- **Microsoft.Identity.Client**: MSAL library

### Voice Recognition
- **Azure Speech Services**: Cognitive services
- **Microsoft.CognitiveServices.Speech**: Speech SDK

### Testing
- **xUnit**: Unit testing framework
- **Moq**: Mocking framework
- **FluentAssertions**: Fluent assertion library
- **Testcontainers**: Integration testing with containers
- **SpecFlow**: BDD testing framework
- **Selenium**: UI automation testing

## Data Flow

1. **Voice Input** → Azure Speech Services (ACL)
2. **Text Extraction** → Application Layer (Commands)
3. **Domain Processing** → Domain Layer (Entities/Value Objects)
4. **Data Persistence** → Infrastructure Layer (MongoDB)
5. **UI Updates** → Presentation Layer (MAUI)

## Security Considerations

- **Authentication**: Azure B2C for user management
- **Authorization**: Role-based access control
- **Data Protection**: Encrypted connections to MongoDB
- **Input Validation**: FluentValidation in Application layer
- **Secrets Management**: Azure Key Vault integration

## Scalability & Performance

- **Async/Await**: Non-blocking operations throughout
- **MongoDB Indexing**: Optimized queries
- **Caching**: In-memory caching for frequently accessed data
- **Connection Pooling**: MongoDB connection management
- **Lazy Loading**: On-demand data loading

## Testing Strategy

### Unit Tests
- Domain logic testing
- Application handler testing
- Value object validation

### Integration Tests
- Repository implementations
- Database operations
- External service integrations

### System Tests
- End-to-end scenarios
- UI automation with Selenium
- BDD scenarios with SpecFlow

## Deployment Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   MAUI Client   │    │  Azure B2C      │    │ Azure Speech    │
│   (Mobile App)  │◄──►│ (Authentication)│    │   Services      │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                                              ▲
         │                                              │
         ▼                                              │
┌─────────────────┐                              ┌─────────────────┐
│   Application   │                              │       ACL       │
│     Layer       │◄────────────────────────────►│   (External     │
└─────────────────┘                              │   Services)     │
         │                                        └─────────────────┘
         ▼
┌─────────────────┐    ┌─────────────────┐
│   Domain        │    │ Infrastructure  │
│   Layer         │◄──►│    Layer        │
└─────────────────┘    └─────────────────┘
                                │
                                ▼
                       ┌─────────────────┐
                       │   MongoDB       │
                       │  (Cosmos DB)    │
                       └─────────────────┘
```

## Best Practices Implemented

1. **SOLID Principles**: Single responsibility, dependency inversion
2. **DDD Tactical Patterns**: Entities, value objects, repositories
3. **Clean Architecture**: Dependency rule, separation of concerns
4. **Testability**: Dependency injection, interface segregation
5. **Error Handling**: Proper exception management
6. **Logging**: Structured logging throughout the application
7. **Configuration**: Environment-specific settings
8. **Documentation**: Comprehensive code documentation