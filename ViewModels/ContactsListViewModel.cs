using PhoneBook.Models;
using PhoneBook.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PhoneBook.ViewModels
{
    public class ContactsListViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditContactCommand { get; } 

        public ContactsListViewModel(IDialogService dialogService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand<Contact>(DeleteContact, CanDeleteContact);
            EditContactCommand = new RelayCommand(EditContact); 
        }

        private void AddContact()
        {
            var newContact = new Contact(Name, Phone);
            if (newContact.Validate())
            {
                if (!Contacts.Any(c => c.Phone == Phone))
                {
                    Contacts.Add(newContact);
                    _dialogService.ShowInfo("Контакт успешно добавлен!");
                    Name = string.Empty;
                    Phone = string.Empty;
                }
                else
                {
                    _dialogService.ShowWarning("Контакт с таким номером уже существует!");
                }
            }
            else
            {
                _dialogService.ShowWarning("Неверный формат имени или телефона!");
            }
        }

        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone);
        }

        private void DeleteContact(Contact contact)
        {
            if (contact != null && _dialogService.ShowConfirmation("Удалить выбранный контакт?"))
            {
                Contacts.Remove(contact);
            }
        }

        private bool CanDeleteContact(Contact contact)
        {
            return contact != null;
        }

        private void EditContact()
        {
            if (SelectedContact != null)
            {
                _navigationService.NavigateTo<ContactEditViewModel>(SelectedContact);
            }
        }
    }
}