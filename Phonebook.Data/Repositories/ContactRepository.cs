using Phonebook.Data.Context;
using Phonebook.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly PhonebookContext _phonebookContext;

        public ContactRepository()
        {
            _phonebookContext = new PhonebookContext();
        }

        public IList<Domain.Model.Contact> GetAll()
        {
            throw new NotImplementedException();
        }

        public Domain.Model.Contact Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Create(Domain.Model.Contact model)
        {
            throw new NotImplementedException();
        }

        public void Update(Domain.Model.Contact model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _phonebookContext.Dispose();
        }
    }
}
