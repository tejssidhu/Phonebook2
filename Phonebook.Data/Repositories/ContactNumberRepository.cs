using Phonebook.Data.Context;
using Phonebook.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data.Repositories
{
    public class ContactNumberRepository : IContactNumberRepository
    {
        private readonly PhonebookContext _phonebookContext;

        public ContactNumberRepository()
        {
            _phonebookContext = new PhonebookContext();
        }
    
        public IList<Domain.Model.ContactNumber> GetAll()
        {
 	        throw new NotImplementedException();
        }

        public Domain.Model.ContactNumber Get(Guid id)
        {
 	        throw new NotImplementedException();
        }

        public Guid Create(Domain.Model.ContactNumber model)
        {
 	        throw new NotImplementedException();
        }

        public void Update(Domain.Model.ContactNumber model)
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
