using AVS.Contacts.Application.Queries;
using AVS.Contacts.Contracts.DTOs;
using MediatR;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AVS.Contacts.Mobile.ViewModels;

public class ContactsPageViewModel : INotifyPropertyChanged
{
    private readonly IMediator _mediator;
    private bool _isLoading;

    public ContactsPageViewModel(IMediator mediator)
    {
        _mediator = mediator;
        Contacts = new ObservableCollection<ContactDto>();
        LoadContactsCommand = new Microsoft.Maui.Controls.Command(async () => await LoadContacts());
    }

    public ObservableCollection<ContactDto> Contacts { get; }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public ICommand LoadContactsCommand { get; }

    public async Task LoadContacts()
    {
        IsLoading = true;
        try
        {
            var contacts = await _mediator.Send(new GetContactsQuery());
            Contacts.Clear();
            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }
        }
        catch (Exception ex)
        {
            await Microsoft.Maui.Controls.Application.Current!.MainPage!.DisplayAlert("Erro", ex.Message, "OK");
        }
        finally
        {
            IsLoading = false;
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