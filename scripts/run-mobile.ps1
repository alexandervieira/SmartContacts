#!/usr/bin/env pwsh

param(
    [string]$Platform = "windows",
    [string]$Configuration = "Debug"
)

Write-Host "📱 Running SmartContacts Mobile App..." -ForegroundColor Green

# Start MongoDB if not running
$mongoRunning = docker ps --filter "name=smartcontacts-mongo" --filter "status=running" -q
if (-not $mongoRunning) {
    Write-Host "🐳 Starting MongoDB..." -ForegroundColor Blue
    docker-compose -f docker/docker-compose.yml up -d mongodb
    Start-Sleep -Seconds 10
}

# Build and run the mobile app
Write-Host "🔨 Building mobile app for $Platform..." -ForegroundColor Yellow

switch ($Platform.ToLower()) {
    "windows" {
        dotnet build src/AVS.Contacts.Mobile --framework net10.0-windows10.0.19041.0 --configuration $Configuration
        if ($LASTEXITCODE -eq 0) {
            dotnet run --project src/AVS.Contacts.Mobile --framework net10.0-windows10.0.19041.0 --configuration $Configuration
        }
    }
    "android" {
        dotnet build src/AVS.Contacts.Mobile --framework net10.0-android --configuration $Configuration
        Write-Host "📱 Deploy to Android device or emulator manually" -ForegroundColor Yellow
    }
    "ios" {
        dotnet build src/AVS.Contacts.Mobile --framework net10.0-ios --configuration $Configuration
        Write-Host "📱 Deploy to iOS device or simulator manually" -ForegroundColor Yellow
    }
    default {
        Write-Host "❌ Unsupported platform: $Platform" -ForegroundColor Red
        Write-Host "Supported platforms: windows, android, ios" -ForegroundColor Yellow
        exit 1
    }
}

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Failed to run mobile app!" -ForegroundColor Red
    exit 1
}

Write-Host "✅ Mobile app started successfully!" -ForegroundColor Green