using PhoneBook.Services;

using System.Windows.Input;

namespace PhoneBook.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public INavigationService NavigationService { get; }

        public ICommand ShowContactsCommand { get; }
        public ICommand ShowAboutCommand { get; }

        public MainWindowViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            ShowContactsCommand = new RelayCommand(() =>
            NavigationService.NavigateTo<ContactsListViewModel>());

            ShowAboutCommand = new RelayCommand(() =>
            NavigationService.NavigateTo<AboutViewModel>());

            NavigationService.NavigateTo<ContactsListViewModel>();
        }
    }
}
