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

        public ContactNumberRepository(Configuration config)
        {
            _phonebookContext = new PhonebookContext(config);
        }
    
        public IList<Domain.Model.ContactNumber> GetAll()
        {
            return _phonebookContext.ContactNumbers;
        }

        public Domain.Model.ContactNumber Get(Guid id)
        {
            return _phonebookContext.ContactNumbers.FirstOrDefault(cn => cn.Id == id);
        }

        public Guid Create(Domain.Model.ContactNumber model)
        {
            model.Id = Guid.NewGuid();

            _phonebookContext.ContactNumbers.Add(model);

            _phonebookContext.SaveContactNumberChanges();

            return model.Id;
        }

        public void Update(Domain.Model.ContactNumber model)
        {
            var contactNumber = Get(model.Id);

            if (contactNumber != null)
            {
                contactNumber.Description = model.Description;
                contactNumber.TelephoneNumber = model.TelephoneNumber;
            }

            _phonebookContext.SaveContactNumberChanges();
        }

        public void Delete(Guid id)
        {
            _phonebookContext.ContactNumbers.Remove(_phonebookContext.ContactNumbers.FirstOrDefault(cn => cn.Id == id));

            _phonebookContext.SaveContactNumberChanges();
        }

        public void Dispose()
        {
            _phonebookContext.Dispose();
        }
    }
}
