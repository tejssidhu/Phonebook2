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

        protected bool Equals(ContactNumber other)
        {
            return ContactId.Equals(other.ContactId) && string.Equals(Description, other.Description) && string.Equals(TelephoneNumber, other.TelephoneNumber);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ContactId.GetHashCode();
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (TelephoneNumber != null ? TelephoneNumber.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}