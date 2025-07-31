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

        private List<Contact> _contacts;

        private IContactDataService _contactDataService;

        private IDialogService _dialogService;

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

        public ICommand BrowseImageCommand { get; private set; }

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        //Конструктор модели-представления
        public ContactsViewModel(IContactDataService contactDataService, IDialogService dialogService)
        {
            _contactDataService = contactDataService;
            _contacts = contactDataService.GetContacts().ToList();
            Contacts = new ObservableCollection<Contact>();
            foreach (Contact contact in _contacts)
            {
                Contacts.Add(contact);
            }
            _dialogService = dialogService;
            UpdateCommand = new RelayCommand(Update);
            EditCommand = new RelayCommand(Edit, CanEdit);
            SaveCommand = new RelayCommand(Save, IsEdit);
            BrowseImageCommand = new RelayCommand(BrowseImage, IsEdit);
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
        }

        private void Add()
        {
            Contact newContact = new Contact
            {
                Name = "N/A",
                PhoneNumbers = new string[2],
                Emails = new string[2],
                Locations = new string[2],
            };

            Contacts.Add(newContact);
            _contacts.Add(newContact);
            SelectedContact = newContact;
        }

        private void Delete()
        {
            _contacts.Remove(SelectedContact);
            Save();
        }

        private bool CanDelete()
        {
            return SelectedContact == null ? false : true;
        }

        private void BrowseImage()
        {
            var filePath = _dialogService.OpenFile("Image files|*.bmp;*.jpg;*.jpeg;*.png|All files");
            SelectedContact.ImagePath = filePath;
            OnPropertyChanged(nameof(SelectedContact));
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
