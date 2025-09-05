using AVS.Contacts.ACL.Configuration;
using AVS.Contacts.Contracts.DTOs;
using AVS.Contacts.Contracts.Services;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace AVS.Contacts.ACL.Services;

public class AzureSpeechService : ISpeechService
{
    private readonly SpeechSettings _settings;

    public AzureSpeechService(IOptions<SpeechSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<string> RecognizeSpeechAsync(Stream audioStream, CancellationToken ct = default)
    {
        var config = SpeechConfig.FromSubscription(_settings.SubscriptionKey, _settings.Region);
        config.SpeechRecognitionLanguage = _settings.Language;

        using var audioConfig = AudioConfig.FromStreamInput(AudioInputStream.CreatePushStream());
        using var recognizer = new SpeechRecognizer(config, audioConfig);

        var result = await recognizer.RecognizeOnceAsync();
        return result.Reason == ResultReason.RecognizedSpeech ? result.Text : string.Empty;
    }

    public Task<VoiceContactDto> ExtractContactInfoAsync(string text, CancellationToken ct = default)
    {
        var nameMatch = Regex.Match(text, @"nome\s+(?:é\s+)?([A-Za-zÀ-ÿ\s]+?)(?:\s+endereço|\s+telefone|$)", RegexOptions.IgnoreCase);
        var addressMatch = Regex.Match(text, @"endereço\s+(?:é\s+)?([^,]+(?:,[^,]+)*?)(?:\s+telefone|$)", RegexOptions.IgnoreCase);
        var phoneMatch = Regex.Match(text, @"telefone\s+(?:é\s+)?([\d\s\(\)\-\+]+)", RegexOptions.IgnoreCase);

        var result = new VoiceContactDto(
            text,
            nameMatch.Success ? nameMatch.Groups[1].Value.Trim() : null,
            addressMatch.Success ? addressMatch.Groups[1].Value.Trim() : null,
            phoneMatch.Success ? phoneMatch.Groups[1].Value.Trim() : null
        );

        return Task.FromResult(result);
    }
}