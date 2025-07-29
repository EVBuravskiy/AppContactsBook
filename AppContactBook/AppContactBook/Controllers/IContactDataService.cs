using AppContactBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppContactBook.Controllers
{
    public interface IContactDataService
    {
        IEnumerable<Contact> GetContacts();
        void SaveContacts(IEnumerable<Contact> contacts);
    }
}
