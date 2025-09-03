# SmartContacts

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![MAUI](https://img.shields.io/badge/MAUI-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/apps/maui)
[![MongoDB](https://img.shields.io/badge/MongoDB-47A248?style=flat-square&logo=mongodb&logoColor=white)](https://www.mongodb.com/)
[![Azure](https://img.shields.io/badge/Azure-0078D4?style=flat-square&logo=microsoftazure&logoColor=white)](https://azure.microsoft.com/)
[![xUnit](https://img.shields.io/badge/xUnit-512BD4?style=flat-square&logo=dotnet)](https://xunit.net/)

## 📋 Table of Contents
- [Overview](#overview)
- [Architecture](#architecture)
- [Technologies](#technologies)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Testing](#testing)
- [Contributing](#contributing)
- [Creator](#creator)
- [License](#license)

## Overview

SmartContacts is a modern contact management application built with .NET MAUI, following Domain-Driven Design (DDD) and Hexagonal Architecture principles. It provides a cross-platform solution for managing contacts with advanced features like speech recognition and secure authentication.

## Architecture

The project follows a clean architecture approach combining DDD tactical patterns with Hexagonal Architecture (Ports & Adapters):

- **Domain Layer**: Core business logic and entities
- **Application Layer**: Use cases and application services
- **Infrastructure Layer**: External concerns and implementations
- **ACL (Anti-Corruption Layer)**: Integration with external services
- **Contracts**: Shared DTOs and interfaces
- **UI Layer**: MAUI cross-platform interface

## Technologies

### Core Framework
- **.NET 10**
- **.NET MAUI**

### Infrastructure
- **MongoDB**: Document database
- **Azure B2C**: Authentication
- **Azure Speech Services**: Voice recognition

### Testing
- **xUnit**: Unit testing framework
- **Moq**: Mocking framework
- **AutoMoq**: Automatic mocking
- **Bogus**: Test data generation
- **FluentAssertions**: Fluent assertions
- **AutoFixture**: Test data automation
- **Testcontainers**: Integration testing
- **SpecFlow**: BDD testing
- **Selenium**: UI testing

## Project Structure

```
📦 SmartContacts
 ┣ 📂 src
 ┃ ┣ 📂 AVS.Contacts.Mobile        # MAUI UI Project
 ┃ ┣ 📂 AVS.Contacts.Domain        # Domain Layer
 ┃ ┣ 📂 AVS.Contacts.Application   # Application Layer
 ┃ ┣ 📂 AVS.Contacts.Infrastructure # Infrastructure Layer
 ┃ ┣ 📂 AVS.Contacts.ACL           # Anti-Corruption Layer
 ┃ ┗ 📂 AVS.Contacts.Contracts     # Shared Contracts
 ┣ 📂 tests
 ┃ ┣ 📂 AVS.Contacts.Tests.Unit       # Unit Tests
 ┃ ┣ 📂 AVS.Contacts.Tests.Integration # Integration Tests
 ┃ ┗ 📂 AVS.Contacts.Tests.System     # System/E2E Tests
 ┣ 📂 docs                         # Documentation
 ┣ 📂 docker                       # Docker configurations
 ┗ 📂 scripts                      # Build and deployment scripts
```

## Getting Started

1. Clone the repository:
```bash
git clone https://github.com/alexandervieira/SmartContacts.git
```

2. Install prerequisites:
   - .NET 10 SDK
   - Visual Studio 2022 Preview
   - MongoDB (or Docker)

3. Build the solution:
```bash
dotnet build
```

4. Run the MAUI application:
```bash
dotnet run --project src/AVS.Contacts.Mobile
```

## Testing

The project includes three levels of testing:

1. **Unit Tests**:
```bash
dotnet test tests/AVS.Contacts.Tests.Unit
```

2. **Integration Tests**:
```bash
dotnet test tests/AVS.Contacts.Tests.Integration
```

3. **System Tests**:
```bash
dotnet test tests/AVS.Contacts.Tests.System
```

## Contributing

1. Read our [contribution guidelines](CONTRIBUTING.md)
2. Fork the project
3. Create your feature branch (`git checkout -b feature/AmazingFeature`)
4. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
5. Push to the branch (`git push origin feature/AmazingFeature`)
6. Open a Pull Request

### Documentation References

- [📝 How to create a Pull Request](https://www.atlassian.com/br/git/tutorials/making-a-pull-request)
- [💾 Commit pattern](https://gist.github.com/joshbuchea/6f47e86d2510bce28f8e7f42ae84c716)

## Creator

- <https://github.com/alexandervsilva>

## License

Code and documentation copyright 2024 by the authors. Code released under the [MIT License](LICENSE).
