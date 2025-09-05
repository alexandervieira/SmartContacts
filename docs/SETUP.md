# SmartContacts Setup Guide

## Prerequisites

### Development Environment
- **Visual Studio 2022 Preview** (17.12 or later)
- **.NET 10 SDK** (Preview)
- **Docker Desktop** (for MongoDB)
- **Git** for version control

### Azure Services (Optional)
- **Azure B2C Tenant** for authentication
- **Azure Speech Services** for voice recognition
- **Azure Cosmos DB** (MongoDB API) for production

## Quick Start

### 1. Clone Repository
```bash
git clone https://github.com/alexandervieira/SmartContacts.git
cd SmartContacts
```

### 2. Start MongoDB with Docker
```bash
# Start MongoDB and Mongo Express
docker-compose -f docker/docker-compose.yml up -d

# Verify MongoDB is running
docker ps
```

### 3. Configure Application Settings

Update `src/AVS.Contacts.Mobile/appsettings.json`:

```json
{
  "MongoSettings": {
    "ConnectionString": "mongodb://admin:password123@localhost:27017",
    "DatabaseName": "SmartContacts",
    "ContactsCollection": "contacts"
  },
  "AzureAdB2C": {
    "Instance": "https://your-tenant.b2clogin.com/",
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id",
    "SignUpSignInPolicyId": "B2C_1_signupsignin",
    "Scopes": [ "https://your-tenant.onmicrosoft.com/api/read" ]
  },
  "AzureSpeech": {
    "SubscriptionKey": "your-speech-key",
    "Region": "brazilsouth",
    "Language": "pt-BR"
  }
}
```

### 4. Build and Run

#### Using PowerShell Scripts
```powershell
# Build solution with tests
.\scripts\build.ps1 -RunTests -StartDocker

# Run mobile app (Windows)
.\scripts\run-mobile.ps1 -Platform windows
```

#### Using .NET CLI
```bash
# Restore packages
dotnet restore

# Build solution
dotnet build

# Run mobile app
dotnet run --project src/AVS.Contacts.Mobile
```

## Detailed Setup

### MongoDB Setup

#### Local Development (Docker)
```bash
# Start services
docker-compose -f docker/docker-compose.yml up -d

# Access Mongo Express (Web UI)
# http://localhost:8081
# Username: admin
# Password: password123
```

#### Production (Azure Cosmos DB)
1. Create Cosmos DB account with MongoDB API
2. Get connection string from Azure Portal
3. Update `MongoSettings:ConnectionString` in configuration

### Azure B2C Setup

#### 1. Create B2C Tenant
1. Go to Azure Portal
2. Create new Azure AD B2C tenant
3. Note the tenant name and ID

#### 2. Register Application
1. In B2C tenant, go to App registrations
2. Create new registration:
   - Name: SmartContacts Mobile
   - Account types: Accounts in any identity provider
   - Redirect URI: `msal{client-id}://auth`
3. Note the Application (client) ID

#### 3. Create User Flow
1. Go to User flows
2. Create new user flow:
   - Type: Sign up and sign in
   - Version: Recommended
   - Name: signupsignin
3. Configure identity providers and user attributes

#### 4. Update Configuration
```json
{
  "AzureAdB2C": {
    "Instance": "https://your-tenant.b2clogin.com/",
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id",
    "SignUpSignInPolicyId": "B2C_1_signupsignin",
    "Scopes": [ "https://your-tenant.onmicrosoft.com/api/read" ]
  }
}
```

### Azure Speech Services Setup

#### 1. Create Speech Resource
1. Go to Azure Portal
2. Create new Speech service resource
3. Choose region (recommend Brazil South for Portuguese)
4. Note the subscription key and region

#### 2. Update Configuration
```json
{
  "AzureSpeech": {
    "SubscriptionKey": "your-speech-key",
    "Region": "brazilsouth",
    "Language": "pt-BR"
  }
}
```

## Development Workflow

### 1. Project Structure
```
SmartContacts/
├── src/                          # Source code
│   ├── AVS.Contacts.Domain/      # Domain layer
│   ├── AVS.Contacts.Application/ # Application layer
│   ├── AVS.Contacts.Infrastructure/ # Infrastructure layer
│   ├── AVS.Contacts.ACL/         # Anti-corruption layer
│   ├── AVS.Contacts.Contracts/   # Shared contracts
│   └── AVS.Contacts.Mobile/      # MAUI mobile app
├── tests/                        # Test projects
│   ├── AVS.Contacts.Tests.Unit/
│   ├── AVS.Contacts.Tests.Integration/
│   └── AVS.Contacts.Tests.System/
├── docker/                       # Docker configurations
├── docs/                         # Documentation
└── scripts/                      # Build scripts
```

### 2. Running Tests

#### Unit Tests
```bash
dotnet test tests/AVS.Contacts.Tests.Unit
```

#### Integration Tests (requires MongoDB)
```bash
# Start MongoDB first
docker-compose -f docker/docker-compose.yml up -d mongodb

# Run integration tests
dotnet test tests/AVS.Contacts.Tests.Integration
```

#### System Tests (BDD with SpecFlow)
```bash
dotnet test tests/AVS.Contacts.Tests.System
```

### 3. Code Quality

#### Static Analysis
```bash
# Run code analysis
dotnet build --verbosity normal

# Check for security vulnerabilities
dotnet list package --vulnerable
```

#### Code Coverage
```bash
# Install coverage tools
dotnet tool install --global dotnet-reportgenerator-globaltool

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Generate coverage report
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage-report
```

## Platform-Specific Setup

### Windows
- Ensure Windows 10 SDK (19041 or later) is installed
- Enable Developer Mode in Windows Settings

### Android
- Install Android SDK through Visual Studio
- Create Android emulator or connect physical device
- Enable USB debugging on device

### iOS/macOS
- Requires macOS with Xcode installed
- Apple Developer account for device deployment
- iOS Simulator for testing

## Troubleshooting

### Common Issues

#### MongoDB Connection Issues
```bash
# Check if MongoDB is running
docker ps | grep mongo

# Check logs
docker logs smartcontacts-mongo

# Restart MongoDB
docker-compose -f docker/docker-compose.yml restart mongodb
```

#### Build Issues
```bash
# Clean solution
dotnet clean

# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore
```

#### MAUI Workload Issues
```bash
# Install MAUI workload
dotnet workload install maui

# Update workloads
dotnet workload update
```

### Performance Optimization

#### MongoDB Indexing
```javascript
// Connect to MongoDB and create indexes
use SmartContacts

// Index on Name for faster searches
db.contacts.createIndex({ "Name.FirstName": 1, "Name.LastName": 1 })

// Index on Phone for lookups
db.contacts.createIndex({ "Phone.Number": 1 })

// Compound index for address searches
db.contacts.createIndex({ 
  "Address.City": 1, 
  "Address.District": 1 
})
```

#### Application Performance
- Enable response compression
- Use connection pooling for MongoDB
- Implement caching for frequently accessed data
- Optimize AutoMapper configurations

## Security Checklist

- [ ] Update default MongoDB credentials
- [ ] Use HTTPS for all external communications
- [ ] Implement proper input validation
- [ ] Store secrets in Azure Key Vault
- [ ] Enable MongoDB authentication
- [ ] Configure CORS policies
- [ ] Implement rate limiting
- [ ] Use secure connection strings
- [ ] Enable audit logging
- [ ] Regular security updates

## Production Deployment

### Azure App Service (Future API)
1. Create App Service plan
2. Deploy application code
3. Configure connection strings
4. Set up continuous deployment

### Azure Container Instances
1. Build Docker image
2. Push to Azure Container Registry
3. Deploy to Container Instances
4. Configure networking and scaling

### Mobile App Distribution
1. Build release versions for each platform
2. Sign applications with certificates
3. Distribute through app stores or enterprise channels
4. Configure app center for analytics and crash reporting