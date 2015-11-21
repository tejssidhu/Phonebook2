using System;
namespace Phonebook.Domain.Model
{
    public class ContactNumber
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public string Description { get; set; }
        public string TelephoneNumber { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ContactNumber cn = (ContactNumber)obj;

            return (ContactId == cn.ContactId) && (Description == cn.Description) && (TelephoneNumber == cn.TelephoneNumber);
        }
    }
}