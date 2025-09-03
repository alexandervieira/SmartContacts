param(
[string]$SolutionName = "MauiContacts"
)


Write-Host "🚀 Criando solução $SolutionName..."

# Estrutura de pastas físicas
New-Item -ItemType Directory -Force -Path src, docs, docker, scripts, tests | Out-Null


# Criar solução vazia
dotnet new sln -n $SolutionName


# ==========================
# 📂 Projetos principais (DDD + Hexagonal)
# ==========================
dotnet new maui -n "$SolutionName.Mobile" -o src/$SolutionName.Mobile
dotnet new classlib -n "$SolutionName.Domain" -o src/$SolutionName.Domain
dotnet new classlib -n "$SolutionName.Application" -o src/$SolutionName.Application
dotnet new classlib -n "$SolutionName.Infrastructure" -o src/$SolutionName.Infrastructure
dotnet new classlib -n "$SolutionName.ACL" -o src/$SolutionName.ACL
dotnet new classlib -n "$SolutionName.Contracts" -o src/$SolutionName.Contracts


# ==========================
# 📂 Projetos de Testes
# ==========================
dotnet new xunit -n "$SolutionName.Tests.Unit" -o tests/$SolutionName.Tests.Unit
dotnet new xunit -n "$SolutionName.Tests.Integration" -o tests/$SolutionName.Tests.Integration
dotnet new xunit -n "$SolutionName.Tests.System" -o tests/$SolutionName.Tests.System


# ==========================
# ➕ Adicionar projetos à solução com pastas lógicas (Solution Folders)
# ==========================
dotnet sln $SolutionName.sln add src/$SolutionName.Mobile --solution-folder UI
dotnet sln $SolutionName.sln add src/$SolutionName.Domain --solution-folder Domain
dotnet sln $SolutionName.sln add src/$SolutionName.Application --solution-folder Application
dotnet sln $SolutionName.sln add src/$SolutionName.Infrastructure --solution-folder Infrastructure
dotnet sln $SolutionName.sln add src/$SolutionName.ACL --solution-folder Infrastructure
dotnet sln $SolutionName.sln add src/$SolutionName.Contracts --solution-folder Contracts


dotnet sln $SolutionName.sln add tests/$SolutionName.Tests.Unit --solution-folder Tests
dotnet sln $SolutionName.sln add tests/$SolutionName.Tests.Integration --solution-folder Tests
dotnet sln $SolutionName.sln add tests/$SolutionName.Tests.System --solution-folder Tests


# ==========================
# 🔗 Referências entre projetos
# ==========================
dotnet add src/$SolutionName.Application reference src/$SolutionName.Domain src/$SolutionName.Contracts
dotnet add src/$SolutionName.Infrastructure reference src/$SolutionName.Contracts src/$SolutionName.Domain
dotnet add src/$SolutionName.ACL reference src/$SolutionName.Contracts
dotnet add src/$SolutionName.Mobile reference src/$SolutionName.Application src/$SolutionName.Contracts src/$SolutionName.Infrastructure src/$SolutionName.ACL


# ==========================
# 📦 Pacotes por camada
# ==========================


# Infra & Mongo
dotnet add src/$SolutionName.Infrastructure package MongoDB.Driver


# Azure Speech
dotnet add src/$SolutionName.ACL package Microsoft.CognitiveServices.Speech


# Autenticação MSAL (Azure B2C)
dotnet add src/$SolutionName.Infrastructure package Microsoft.Identity.Client


# Config JSON no MAUI
dotnet add src/$SolutionName.Mobile package Microsoft.Extensions.Configuration.Json


# ==========================
# 📦 Testes Unitários
# ==========================
dotnet add tests/$SolutionName.Tests.Unit package Moq
dotnet add tests/$SolutionName.Tests.Unit package AutoMoq
dotnet add tests/$SolutionName.Tests.Unit package Bogus
dotnet add tests/$SolutionName.Tests.Unit package FluentAssertions
dotnet add tests/$SolutionName.Tests.Unit package AutoFixture


# ==========================
# 📦 Testes de Integração
# ==========================
dotnet add tests/$SolutionName.Tests.Integration package Testcontainers.MongoDb
dotnet add tests/$SolutionName.Tests.Integration package Bogus
dotnet add tests/$SolutionName.Tests.Integration package FluentAssertions


# ==========================
# 📦 Testes de Sistema (BDD + Selenium)
# ==========================
dotnet add tests/$SolutionName.Tests.System package Selenium.WebDriver
dotnet add tests/$SolutionName.Tests.System package Selenium.WebDriver.MSEdgeDriver
dotnet add tests/$SolutionName.Tests.System package SpecFlow
dotnet add tests/$SolutionName.Tests.System package SpecFlow.xUnit
dotnet add tests/$SolutionName.Tests.System package SpecFlow.Tools.MsBuild.Generation


Write-Host "✅ Solução $SolutionName criada com sucesso!"
Write-Host "Abra no Visual Studio 2022 Preview e rode: dotnet build"