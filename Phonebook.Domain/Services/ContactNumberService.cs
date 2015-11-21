using Phonebook.Domain.Exceptions;
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
        private readonly IContactService _contactService;

        public ContactNumberService(IContactNumberRepository contactNumberRepository, IContactService contactService)
        {
            _contactNumberRepository = contactNumberRepository;
            _contactService = contactService;
        }

        public IList<Model.ContactNumber> GetAll()
        {
            return _contactNumberRepository.GetAll();
        }

        public Model.ContactNumber Get(Guid id)
        {
            return _contactNumberRepository.Get(id);
        }

        public Guid Create(Model.ContactNumber model)
        {
            var contact = _contactService.Get(model.ContactId);

            if (contact == null) throw new ObjectNotFoundException("Contact");

            var contactNumber = _contactNumberRepository.GetAll().SingleOrDefault(c => c.ContactId == model.ContactId && c.TelephoneNumber == model.TelephoneNumber);

            if (contactNumber != null) throw new ObjectAlreadyExistException("Contact Number", "telephone number");

            var id = _contactNumberRepository.Create(model);

            return id;
        }

        public void Update(Model.ContactNumber model)
        {
            if (_contactNumberRepository.GetAll().Any(c => c.ContactId == model.ContactId && c.Id != model.Id && c.TelephoneNumber == model.TelephoneNumber))
            {
                throw new ObjectAlreadyExistException("Contact Number", "telephone number");
            }

            _contactNumberRepository.Update(model);
        }

        public void Delete(Guid id)
        {
            _contactNumberRepository.Delete(id);
        }

        public void Dispose()
        {
            _contactNumberRepository.Dispose();
        }
    }
}
