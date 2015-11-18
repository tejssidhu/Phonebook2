using System;
using System.CodeDom;
using System.Collections.Generic;

namespace Phonebook.Domain.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Contact> PhoneBook { get; set; }

        public User()
        {
            if (PhoneBook == null)
                PhoneBook = new List<Contact>();
        }
    }
}
