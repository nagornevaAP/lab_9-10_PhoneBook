

namespace PhoneBook.ViewModels
{
    public class AboutViewModel : ObservableObject
    {
        public string AppName => "Телефонная книга MVVM";
        public string Version => "Версия 11.0 (ViewModel-First Navigation)";
        public string Description => "Приложение демонстрирует навигацию с помощью подхода ViewModel-First";
    }
}
