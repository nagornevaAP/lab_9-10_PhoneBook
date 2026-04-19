using PhoneBook.Models;
using PhoneBook.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PhoneBook.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;

        public ObservableCollection<Contact> Contacts { get; }

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

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand<Contact>(DeleteContact, CanDeleteContact);
        }

        private void AddContact()
        {
            if (Contacts.Any(c => c.Phone == Phone))
            {
                _dialogService.ShowWarning(
                "Контакт с таким номером телефона уже существует!",
                "Дубликат");
                return;
            }

            var contact = new Contact(Name, Phone);

            if (contact.Validate())
            {
                Contacts.Add(contact);

                _dialogService.ShowInfo(
                $"Контакт '{Name}' успешно добавлен!",
                "Успех");


                Name = string.Empty;
                Phone = string.Empty;
            }
        }

        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone);
        }

        private void DeleteContact(Contact contact)
        {
            if (contact == null) return;

            bool confirmed = _dialogService.ShowConfirmation(
            $"Вы действительно хотите удалить контакт '{contact.Name}'?",
            "Подтверждение удаления");

            if (confirmed)
            {
                Contacts.Remove(contact);
            }
        }

        private bool CanDeleteContact(Contact contact)
        {
            return contact != null;
        }
    }
}