#!/usr/bin/env pwsh

param(
    [string]$Configuration = "Release",
    [switch]$RunTests = $false,
    [switch]$StartDocker = $false
)

Write-Host "🚀 Building SmartContacts Solution..." -ForegroundColor Green

# Start Docker services if requested
if ($StartDocker) {
    Write-Host "🐳 Starting Docker services..." -ForegroundColor Blue
    docker-compose -f docker/docker-compose.yml up -d
    Start-Sleep -Seconds 10
}

# Clean and restore
Write-Host "🧹 Cleaning solution..." -ForegroundColor Yellow
dotnet clean

Write-Host "📦 Restoring packages..." -ForegroundColor Yellow
dotnet restore

# Build solution
Write-Host "🔨 Building solution in $Configuration mode..." -ForegroundColor Yellow
dotnet build --configuration $Configuration --no-restore

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Build failed!" -ForegroundColor Red
    exit 1
}

# Run tests if requested
if ($RunTests) {
    Write-Host "🧪 Running tests..." -ForegroundColor Blue
    
    Write-Host "Running Unit Tests..." -ForegroundColor Cyan
    dotnet test tests/AVS.Contacts.Tests.Unit --configuration $Configuration --no-build --verbosity normal
    
    Write-Host "Running Integration Tests..." -ForegroundColor Cyan
    dotnet test tests/AVS.Contacts.Tests.Integration --configuration $Configuration --no-build --verbosity normal
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Tests failed!" -ForegroundColor Red
        exit 1
    }
}

Write-Host "✅ Build completed successfully!" -ForegroundColor Green