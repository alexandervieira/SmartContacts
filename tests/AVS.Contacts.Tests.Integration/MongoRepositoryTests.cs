using AVS.Contacts.Domain.Entities;
using AVS.Contacts.Domain.ValueObjects;
using AVS.Contacts.Infrastructure.Mongo.Configuration;
using AVS.Contacts.Infrastructure.Mongo.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Testcontainers.MongoDb;

namespace AVS.Contacts.Tests.Integration;

public class MongoRepositoryTests : IAsyncLifetime
{
    private readonly MongoDbContainer _mongoContainer = new MongoDbBuilder()
        .WithImage("mongo:7.0")
        .Build();

    private ContactRepository _repository = null!;

    public async Task InitializeAsync()
    {
        await _mongoContainer.StartAsync();

        var settings = Options.Create(new MongoSettings
        {
            ConnectionString = _mongoContainer.GetConnectionString(),
            DatabaseName = "TestDb",
            ContactsCollection = "contacts"
        });

        _repository = new ContactRepository(settings);
    }

    public async Task DisposeAsync()
    {
        await _mongoContainer.DisposeAsync();
    }

    [Fact]
    public async Task AddAsync_Should_Persist_Contact()
    {
        // Arrange
        var name = new Name("João", "Silva");
        var address = new Address("Rua A", "123", "Centro", "São Paulo");
        var phone = PhoneNumber.Create("11999999999");
        var contact = new Contact(name, address, phone);

        // Act
        var result = await _repository.AddAsync(contact);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();

        var retrieved = await _repository.GetByIdAsync(result.Id);
        retrieved.Should().NotBeNull();
        retrieved!.Name.FirstName.Should().Be("João");
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Contacts()
    {
        // Arrange
        var contact1 = new Contact(new Name("João", "Silva"), new Address("Rua A", "123", "Centro", "SP"), PhoneNumber.Create("11999999999"));
        var contact2 = new Contact(new Name("Maria", "Santos"), new Address("Rua B", "456", "Jardim", "RJ"), PhoneNumber.Create("21888888888"));

        await _repository.AddAsync(contact1);
        await _repository.AddAsync(contact2);

        // Act
        var contacts = await _repository.GetAllAsync();

        // Assert
        contacts.Should().HaveCountGreaterThanOrEqualTo(2);
        contacts.Should().Contain(c => c.Name.FirstName == "João");
    }
}