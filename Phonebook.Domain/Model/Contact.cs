using System;
using System.Collections.Generic;

namespace Phonebook.Domain.Model
{
    public class Contact
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<ContactNumber> ContactNumbers { get; set; }

        public Contact()
        {
            if (ContactNumbers == null)
                ContactNumbers = new List<ContactNumber>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Contact c = (Contact)obj;

            return (UserId == c.UserId) && (Title == c.Title) && (Forename == c.Forename) && (Surname == c.Surname) && (Email == c.Email);
        }
    }
}
