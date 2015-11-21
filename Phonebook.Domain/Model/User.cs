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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            User u = (User)obj;

            return (Username == u.Username) && (Password == u.Password);
        }

        protected bool Equals(User other)
        {
            return string.Equals(Username, other.Username) && string.Equals(Password, other.Password);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Username != null ? Username.GetHashCode() : 0)*397) ^ (Password != null ? Password.GetHashCode() : 0);
            }
        }
    }
}
