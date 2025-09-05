using AVS.Contacts.Contracts.DTOs;
using MediatR;

namespace AVS.Contacts.Application.Commands;

public record CreateContactCommand(CreateContactDto Contact) : IRequest<ContactDto>;

public record CreateContactFromVoiceCommand(VoiceContactDto VoiceData) : IRequest<ContactDto>;