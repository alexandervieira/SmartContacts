using AutoMapper;
using AVS.Contacts.Application.Commands;
using AVS.Contacts.Application.Queries;
using AVS.Contacts.Contracts.DTOs;
using AVS.Contacts.Contracts.Services;
using AVS.Contacts.Domain.Entities;
using AVS.Contacts.Domain.Repositories;
using AVS.Contacts.Domain.ValueObjects;
using MediatR;

namespace AVS.Contacts.Application.Handlers;

public class CreateContactHandler : IRequestHandler<CreateContactCommand, ContactDto>
{
    private readonly IContactRepository _repository;
    private readonly IMapper _mapper;

    public CreateContactHandler(IContactRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ContactDto> Handle(CreateContactCommand request, CancellationToken ct)
    {
        var name = new Name(request.Contact.FirstName, request.Contact.LastName);
        var address = new Address(request.Contact.Street, request.Contact.Number, request.Contact.District, request.Contact.City);
        var phone = PhoneNumber.Create(request.Contact.Phone);

        var contact = new Contact(name, address, phone);
        await _repository.AddAsync(contact, ct);

        return _mapper.Map<ContactDto>(contact);
    }
}

public class CreateContactFromVoiceHandler : IRequestHandler<CreateContactFromVoiceCommand, ContactDto>
{
    private readonly ISpeechService _speechService;
    private readonly IMediator _mediator;

    public CreateContactFromVoiceHandler(ISpeechService speechService, IMediator mediator)
    {
        _speechService = speechService;
        _mediator = mediator;
    }

    public async Task<ContactDto> Handle(CreateContactFromVoiceCommand request, CancellationToken ct)
    {
        var extractedData = await _speechService.ExtractContactInfoAsync(request.VoiceData.RawText, ct);

        var parts = extractedData.ExtractedName?.Split(' ') ?? ["", ""];
        var addressParts = extractedData.ExtractedAddress?.Split(',') ?? ["", "", "", ""];

        var createDto = new CreateContactDto(
            parts.FirstOrDefault() ?? "",
            string.Join(" ", parts.Skip(1)),
            addressParts.ElementAtOrDefault(0) ?? "",
            addressParts.ElementAtOrDefault(1) ?? "",
            addressParts.ElementAtOrDefault(2) ?? "",
            addressParts.ElementAtOrDefault(3) ?? "",
            extractedData.ExtractedPhone ?? ""
        );

        return await _mediator.Send(new CreateContactCommand(createDto), ct);
    }
}

public class GetContactsHandler : IRequestHandler<GetContactsQuery, IEnumerable<ContactDto>>
{
    private readonly IContactRepository _repository;
    private readonly IMapper _mapper;

    public GetContactsHandler(IContactRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContactDto>> Handle(GetContactsQuery request, CancellationToken ct)
    {
        var contacts = await _repository.GetAllAsync(ct);
        return _mapper.Map<IEnumerable<ContactDto>>(contacts);
    }
}

public class GetContactByIdHandler : IRequestHandler<GetContactByIdQuery, ContactDto?>
{
    private readonly IContactRepository _repository;
    private readonly IMapper _mapper;

    public GetContactByIdHandler(IContactRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ContactDto?> Handle(GetContactByIdQuery request, CancellationToken ct)
    {
        var contact = await _repository.GetByIdAsync(request.Id, ct);
        return contact is null ? null : _mapper.Map<ContactDto>(contact);
    }
}