using AVS.Contacts.Mobile.ViewModels;
using AVS.Contacts.Mobile.Views;

namespace AVS.Contacts.Mobile;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnContactsClicked(object sender, EventArgs e)
    {
        var contactsPage = Handler?.MauiContext?.Services.GetService<ContactsPage>();
        if (contactsPage != null)
        {
            await Navigation.PushAsync(contactsPage);
        }
    }
}