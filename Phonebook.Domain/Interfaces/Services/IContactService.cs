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
		IQueryable<Contact> Search(Guid userId, string name, string email);
		IQueryable<Contact> GetAllByUserId(Guid userId);
		IQueryable<Contact> SearchContactsByName(Guid userId, string forename, string surname);
		IQueryable<Contact> SearchContactsByEmail(Guid userId, string email);
    }
}
