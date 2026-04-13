using System.Text.RegularExpressions;
using PhoneBook.ViewModels;

namespace PhoneBook.Models
{
    public class Contact : ObservableObject
    {
        private string _name = string.Empty;
        private string _phone = string.Empty;

        public Contact(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (Set(ref _name, value))
                    Validate();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (Set(ref _phone, value))
                    Validate();
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            string pattern = @"^\+?7?\d{10}$";
            if (!Regex.IsMatch(Phone, pattern))
                return false;

            return true;
        }
    }
}