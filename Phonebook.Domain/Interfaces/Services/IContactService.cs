using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Domain.Interfaces.Services
{
    public interface IContactService : IService<Contact>
    {
        IList<Contact> GetAllByUserId(Guid userId);
        IList<Contact> SearchContactsByName(Guid userId, string forename, string surname);
        IList<Contact> SearchContactsByEmail(Guid userId, string email);
    }
}
