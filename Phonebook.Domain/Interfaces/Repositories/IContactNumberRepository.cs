using Phonebook.Domain.Model;
using System;

namespace Phonebook.Domain.Interfaces.Repositories
{
    public interface IContactNumberRepository : IRepository<ContactNumber>
    {
        void DeleteContactNumbersByContactId(Guid contactId);
    }
}
