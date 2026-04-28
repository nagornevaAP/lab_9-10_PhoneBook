using PhoneBook.Models;
using PhoneBook.Services;

using System.Windows.Input;

namespace PhoneBook.ViewModels
{
    public class ContactEditViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private Contact _contact;

        public string EditName
        {
            get => _contact?.Name ?? "";
            set
            {
                if (_contact != null)
                {
                    _contact.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EditPhone
        {
            get => _contact?.Phone ?? "";
            set
            {
                if (_contact != null)
                {
                    _contact.Phone = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ContactEditViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Contact contact)
            {
                _contact = contact;
                OnPropertyChanged(nameof(EditName));
                OnPropertyChanged(nameof(EditPhone));
            }
        }

        private void Save()
        {
            _navigationService.NavigateTo<ContactsListViewModel>();
        }

        private void Cancel()
        {
            _navigationService.NavigateTo<ContactsListViewModel>();
        }
    }
}
