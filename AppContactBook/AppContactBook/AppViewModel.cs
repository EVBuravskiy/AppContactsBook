using AppContactBook.Controllers;
using AppContactBook.Utilities;
using AppContactBook.ViewModels.AppContactBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppContactBook
{
    public class AppViewModel : ObservableObject
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { OnPropertyChanged(ref _currentView, value); }
        }

        private BookViewModel _bookViewModel;
        public BookViewModel BookViewModel
        {
            get { return _bookViewModel; }
            set { OnPropertyChanged(ref _bookViewModel, value); }
        }

        public AppViewModel()
        {
            IContactDataService contactDataService = new MockDataController();
            IDialogService dialogService = new WindowDialogService();

            BookViewModel = new BookViewModel(contactDataService, dialogService);
            CurrentView = BookViewModel;
        }
    }
}
