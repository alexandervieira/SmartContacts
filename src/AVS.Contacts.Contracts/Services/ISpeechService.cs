using AVS.Contacts.Contracts.DTOs;

namespace AVS.Contacts.Contracts.Services;

public interface ISpeechService
{
    Task<string> RecognizeSpeechAsync(Stream audioStream, CancellationToken ct = default);
    Task<VoiceContactDto> ExtractContactInfoAsync(string text, CancellationToken ct = default);
}