using AppContactBook.Controllers;
using AppContactBook.Models;
using AppContactBook.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppContactBook.ViewModels
{
    public class ContactsViewModel : ObservableObject
    {
        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set { OnPropertyChanged(ref _selectedContact, value); }
        }
        public ObservableCollection<Contact> Contacts { get; private set; }

        IContactDataService _contactDataService;

        //Конструктор модели-представления
        public ContactsViewModel(IContactDataService contactDataService)
        {
            _contactDataService = contactDataService;  
        }

        public void LoadContacts(IEnumerable<Contact> contacts)
        {
            Contacts = new ObservableCollection<Contact>(contacts);
            OnPropertyChanged(nameof(Contacts));
        }

    }
}
