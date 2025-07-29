using AppContactBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppContactBook.Controllers
{
    public class MockDataController : IContactDataService
    {
        private IEnumerable<Contact> _contacts;

        public MockDataController()
        {
            _contacts = new List<Contact>()
            {
                new Contact()
                {
                    Name="John Brave",
                    PhoneNumbers = new string[]
                    {
                        "555-111-11111",
                        "555-222-22222",
                    },
                    Emails = new string[]
                    {
                        "johnbrave@personal.com",
                        "jb_programmer@business.com"
                    },
                    Locations = new string[]
                    {
                        "111 Fake Street",
                        "222 Fake Ave"
                    }
                },
                new Contact()
                {
                    Name="Elena Beautiful",
                    PhoneNumbers = new string[]
                    {
                        "555-333-33333",
                        "555-444-44444",
                    },
                    Emails = new string[]
                    {
                        "elena_b@personal.com",
                        "elena_topmodel@business.com"
                    },
                    Locations = new string[]
                    {
                        "111 Fake Street",
                        "333 Fake Ave"
                    }
                }
            };
        }
        public IEnumerable<Contact> GetContacts()
        {

            return _contacts;
        }

        public void SaveContacts(IEnumerable<Contact> contacts)
        {
            _contacts = contacts;
        }
    }

}
