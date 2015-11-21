using System;
namespace Phonebook.Domain.Model
{
    public class ContactNumber
    {
        public Guid id { get; set; }
        public Guid ContactId { get; set; }
        public string Description { get; set; }
        public string TelephoneNumber { get; set; }
    }
}