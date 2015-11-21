using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Domain.Services
{
    public class ContactNumberService : IContactNumberService
    {
        private readonly IContactNumberRepository _contactNumberRepository;

        public ContactNumberService(IContactNumberRepository contactNumberRepository)
        {
            _contactNumberRepository = contactNumberRepository;
        }

        public IList<Model.ContactNumber> GetAll()
        {
            throw new NotImplementedException();
        }

        public Model.ContactNumber Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Create(Model.ContactNumber model)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.ContactNumber model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _contactNumberRepository.Dispose();
        }
    }
}
