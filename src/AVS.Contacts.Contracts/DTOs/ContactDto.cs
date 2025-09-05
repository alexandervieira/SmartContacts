namespace AVS.Contacts.Contracts.DTOs;

public record ContactDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Street,
    string Number,
    string District,
    string City,
    string Phone,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);

public record CreateContactDto(
    string FirstName,
    string LastName,
    string Street,
    string Number,
    string District,
    string City,
    string Phone
);

public record UpdateContactDto(
    string? FirstName = null,
    string? LastName = null,
    string? Street = null,
    string? Number = null,
    string? District = null,
    string? City = null,
    string? Phone = null
);

public record VoiceContactDto(
    string RawText,
    string? ExtractedName = null,
    string? ExtractedAddress = null,
    string? ExtractedPhone = null
);