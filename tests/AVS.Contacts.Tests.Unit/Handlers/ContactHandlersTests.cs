using AutoMapper;
using AVS.Contacts.Application.Commands;
using AVS.Contacts.Application.Handlers;
using AVS.Contacts.Application.Mappings;
using AVS.Contacts.Contracts.DTOs;
using AVS.Contacts.Domain.Entities;
using AVS.Contacts.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace AVS.Contacts.Tests.Unit.Handlers;

public class ContactHandlersTests
{
    private readonly Mock<IContactRepository> _repositoryMock;
    private readonly IMapper _mapper;

    public ContactHandlersTests()
    {
        _repositoryMock = new Mock<IContactRepository>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<ContactProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task CreateContactHandler_Should_Create_Contact_Successfully()
    {
        // Arrange
        var createDto = new CreateContactDto("João", "Silva", "Rua A", "123", "Centro", "São Paulo", "11999999999");
        var command = new CreateContactCommand(createDto);
        var handler = new CreateContactHandler(_repositoryMock.Object, _mapper);

        _repositoryMock.Setup(x => x.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync((Contact c, CancellationToken ct) => c);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be("João");
        result.LastName.Should().Be("Silva");
        _repositoryMock.Verify(x => x.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}