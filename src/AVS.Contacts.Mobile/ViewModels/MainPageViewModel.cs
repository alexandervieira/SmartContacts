using AVS.Contacts.Application.Commands;
using AVS.Contacts.Contracts.DTOs;
using AVS.Contacts.Contracts.Services;
using MediatR;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AVS.Contacts.Mobile.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    private readonly IMediator _mediator;
    private readonly ISpeechService _speechService;
    private string _voiceText = string.Empty;
    private bool _isListening;

    public MainPageViewModel(IMediator mediator, ISpeechService speechService)
    {
        _mediator = mediator;
        _speechService = speechService;
        StartListeningCommand = new Command(async () => await StartListening());
        CreateContactCommand = new Command(async () => await CreateContact());
    }

    public string VoiceText
    {
        get => _voiceText;
        set => SetProperty(ref _voiceText, value);
    }

    public bool IsListening
    {
        get => _isListening;
        set => SetProperty(ref _isListening, value);
    }

    public Command StartListeningCommand { get; }
    public Command CreateContactCommand { get; }

    private async Task StartListening()
    {
        IsListening = true;
        try
        {
            // Simulate voice input - in real app, capture from microphone
            VoiceText = "Meu nome é João Silva, endereço Rua das Flores 123, Jardim Paulista, São Paulo, telefone 11 99999-9999";
        }
        finally
        {
            IsListening = false;
        }
    }

    private async Task CreateContact()
    {
        if (string.IsNullOrWhiteSpace(VoiceText)) return;

        try
        {
            var voiceData = new VoiceContactDto(VoiceText);
            var contact = await _mediator.Send(new CreateContactFromVoiceCommand(voiceData));

            await Microsoft.Maui.Controls.Application.Current!.MainPage!.DisplayAlert("Sucesso",
                $"Contato {contact.FirstName} {contact.LastName} criado com sucesso!", "OK");

            VoiceText = string.Empty;
        }
        catch (Exception ex)
        {
            await Microsoft.Maui.Controls.Application.Current!.MainPage!.DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}