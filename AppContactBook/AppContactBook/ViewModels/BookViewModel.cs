using AppContactBook.Controllers;
using AppContactBook.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppContactBook.ViewModels
{
    namespace AppContactBook.ViewModels
    {
        public class BookViewModel : ObservableObject
        {
            private ContactsViewModel _contactsViewModel;
            public ContactsViewModel ContactsViewModel
            {
                get { return _contactsViewModel; }
                set { OnPropertyChanged(ref _contactsViewModel, value); }
            }

            private IDialogService _dialogService;

            public ICommand LoadContactsCommand { get; private set; }
            public ICommand LoadFavoriteCommand { get; private set; }

            private IContactDataService _contactDataService;

            public BookViewModel(IContactDataService contactDataService, IDialogService dialogService)
            {
                ContactsViewModel = new ContactsViewModel(contactDataService, dialogService);
                LoadContactsCommand = new RelayCommand(LoadContacts);
                LoadFavoriteCommand = new RelayCommand(LoadFavorites);
                _contactDataService = contactDataService;
                _dialogService = dialogService;
            }

            private void LoadContacts()
            {
                ContactsViewModel.LoadContacts(_contactDataService.GetContacts());
            }

            private void LoadFavorites()
            {
                var favorites = _contactDataService.GetContacts().Where(c => c.IsFavorite);
                ContactsViewModel.LoadContacts(favorites);
            }
        }
    }
}
