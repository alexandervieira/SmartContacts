using AVS.Contacts.Mobile.ViewModels;

namespace AVS.Contacts.Mobile.Views;

public partial class ContactsPage : ContentPage
{
    public ContactsPage(ContactsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ContactsPageViewModel viewModel)
        {
            await viewModel.LoadContacts();
        }
    }
}