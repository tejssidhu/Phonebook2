using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Interfaces.UnitOfWork;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Domain.Services
{
    public class ContactService : IContactService
    {
		private readonly IUnitOfWork _unitOfWork;

		public ContactService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
        }

		public IQueryable<Contact> GetAll()
        {
			return _unitOfWork.ContactRepository.GetAll();
        }

        public Contact Get(Guid id)
        {
			return _unitOfWork.ContactRepository.Get(id);
        }

        public Guid Create(Contact model)
        {
			var user = _unitOfWork.UserRepository.Get(model.UserId);

            if (user == null) throw new ObjectNotFoundException("User");

			var contact = _unitOfWork.ContactRepository.GetAll().SingleOrDefault(c => c.UserId == model.UserId && c.Email == model.Email);

            if (contact != null) throw new ObjectAlreadyExistException("Contact", "Email");

			var id = _unitOfWork.ContactRepository.Create(model);

			_unitOfWork.SaveChanges();

            return id;
        }

        public void Update(Contact model)
        {
			if (_unitOfWork.ContactRepository.GetAll().Any(c => c.UserId == model.UserId && c.Id != model.Id && c.Email == model.Email))
            {
                throw new ObjectAlreadyExistException("Contact", "Email");
            }

			_unitOfWork.ContactRepository.Update(model);

			_unitOfWork.SaveChanges();
        }

        public void Delete(Guid id)
        {
			_unitOfWork.ContactRepository.Delete(id);

			_unitOfWork.SaveChanges();
        }

        public void Dispose()
        {
			_unitOfWork.Dispose();
        }

		public IQueryable<Contact> SearchContactsByName(Guid userId, string forename, string surname)
        {
			return _unitOfWork.ContactRepository.GetAll().Where(x => x.UserId == userId && x.Forename.Contains(forename) && x.Surname.Contains(surname));
        }

		public IQueryable<Contact> SearchContactsByEmail(Guid userId, string email)
        {
			return _unitOfWork.ContactRepository.GetAll().Where(x => x.UserId == userId && x.Email.Contains(email));
        }

		public IQueryable<Contact> GetAllByUserId(Guid userId)
        {
			return _unitOfWork.ContactRepository.GetAll().Where(x => x.UserId == userId);
        }

		public IQueryable<Contact> Search(Guid userId, string name, string email)
        {
            name = String.IsNullOrEmpty(name) ? "" : name;
            email = String.IsNullOrEmpty(email) ? "" : email;
			return _unitOfWork.ContactRepository.GetAll().Where(x => x.UserId == userId && (x.Forename + " " + x.Surname).Contains(name) && x.Email.Contains(email));
        }
    }
}
