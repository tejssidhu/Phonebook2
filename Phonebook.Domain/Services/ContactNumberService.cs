using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Domain.Services
{
    public class ContactNumberService : IContactNumberService
    {
		private readonly IUnitOfWork _unitOfWork;

		public ContactNumberService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
        }

		public IQueryable<Model.ContactNumber> GetAll()
        {
			return _unitOfWork.ContactNumberRepository.GetAll();
        }

        public Model.ContactNumber Get(Guid id)
        {
			return _unitOfWork.ContactNumberRepository.Get(id);
        }

        public Guid Create(Model.ContactNumber model)
        {
			var contact = _unitOfWork.ContactRepository.Get(model.ContactId);

            if (contact == null) throw new ObjectNotFoundException("Contact");

			var contactNumber = _unitOfWork.ContactNumberRepository.GetAll().SingleOrDefault(c => c.ContactId == model.ContactId && c.TelephoneNumber == model.TelephoneNumber);

            if (contactNumber != null) throw new ObjectAlreadyExistException("Contact Number", "telephone number");

			var id = _unitOfWork.ContactNumberRepository.Create(model);

			_unitOfWork.SaveChanges();

            return id;
        }

        public void Update(Model.ContactNumber model)
        {
            if (_unitOfWork.ContactNumberRepository.GetAll().Any(c => c.ContactId == model.ContactId && c.Id != model.Id && c.TelephoneNumber == model.TelephoneNumber))
            {
                throw new ObjectAlreadyExistException("Contact Number", "telephone number");
            }

            _unitOfWork.ContactNumberRepository.Update(model);

			_unitOfWork.SaveChanges();
        }
        
        public void Delete(Guid id)
        {
            _unitOfWork.ContactNumberRepository.Delete(id);

			_unitOfWork.SaveChanges();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IQueryable<Model.ContactNumber> GetAllByContactId(Guid contactId)
        {
			return _unitOfWork.ContactNumberRepository.GetAll().Where(x => x.ContactId == contactId);
        }
    }
}
