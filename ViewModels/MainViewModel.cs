using System.Collections.ObjectModel;
using System.Windows.Input;
using PhoneBook.Models;

namespace PhoneBook.ViewModels
{
    public class MainViewModel : ObservableObject
    {
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
            set
            {
                Set(ref _selectedContact, value);
                (DeleteCommand as RelayCommand<Contact>)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand<Contact>(DeleteContact, CanDeleteContact);
        }

        private void AddContact()
        {
            var contact = new Contact(Name, Phone);

            if (contact.Validate()) 
            {
                Contacts.Add(contact);
                Name = string.Empty;
                Phone = string.Empty;
            }
        }


        private bool CanAddContact()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone))
                return false;
            var tempContact = new Contact(Name, Phone);
            return tempContact.Validate(); 
        }


        private void DeleteContact(Contact contact)
        {
            if (contact != null)
                Contacts.Remove(contact);
        }

        private bool CanDeleteContact(Contact contact)
        {
            return contact != null;
        }
    }
}