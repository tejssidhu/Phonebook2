using System.Collections.Generic;

namespace Phonebook.Domain.Model
{
    public class Contact
    {
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
    }
}
