using AppContactBook.Controllers;
using AppContactBook.Models;
using AppContactBook.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
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

        private IEnumerable<Contact> _contacts;

        private IContactDataService _contactDataService;

        public ICommand UpdateCommand { get; private set; }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                OnPropertyChanged(ref _isEditMode, value);
                OnPropertyChanged(nameof(IsDisplayMode));
            }
        }

        public bool IsDisplayMode
        {
            get { return !_isEditMode; }

        }

        public ICommand EditCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        //Конструктор модели-представления
        public ContactsViewModel(IContactDataService contactDataService)
        {
            _contactDataService = contactDataService;
            UpdateCommand = new RelayCommand(Update);
            _contacts = contactDataService.GetContacts();
            EditCommand = new RelayCommand(Edit, CanEdit);
            SaveCommand = new RelayCommand(Save, IsEdit);
        }

        private void Update()
        {
            _contactDataService.SaveContacts(_contacts);
        }

        private void Save()
        {
            _contactDataService.SaveContacts(_contacts);
            IsEditMode = false;
            OnPropertyChanged(nameof(SelectedContact));
        }

        private bool IsEdit()
        {
            return IsEditMode;
        }

        private bool CanEdit()
        {
            if(SelectedContact == null)
            {
                return false;
            }
            return !IsEditMode;
        }

        private void Edit()
        {
            IsEditMode = true;
        }

        public void LoadContacts(IEnumerable<Contact> contacts)
        {
            Contacts = new ObservableCollection<Contact>(contacts);
            OnPropertyChanged(nameof(Contacts));
        }

    }
}
