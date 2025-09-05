using AVS.Contacts.Contracts.DTOs;
using MediatR;

namespace AVS.Contacts.Application.Queries;

public record GetContactsQuery : IRequest<IEnumerable<ContactDto>>;

public record GetContactByIdQuery(Guid Id) : IRequest<ContactDto?>;