using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Domain.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUserService _userService;

        public ContactService(IContactRepository contactRepository, IUserService userService)
        {
            _contactRepository = contactRepository;
            _userService = userService;
        }

        public IList<Contact> GetAll()
        {
            return _contactRepository.GetAll();
        }

        public Contact Get(Guid id)
        {
            return _contactRepository.Get(id);
        }

        public Guid Create(Contact model)
        {
            var user = _userService.Get(model.UserId);

            if (user == null) throw new ObjectNotFoundException("User");

            var contact = _contactRepository.GetAll().SingleOrDefault(c => c.UserId == model.UserId && c.Email == model.Email);

            if (contact != null) throw new ObjectAlreadyExistException("Contact", "Email");

            var id = _contactRepository.Create(model);

            return id;
        }

        public void Update(Contact model)
        {
            if (_contactRepository.GetAll().Any(c => c.UserId == model.UserId && c.Id != model.Id && c.Email == model.Email))
            {
                throw new ObjectAlreadyExistException("Contact", "Email");
            }

            _contactRepository.Update(model);
        }

        public void Delete(Guid id)
        {
            _contactRepository.Delete(id);
        }

        public void Dispose()
        {
            _contactRepository.Dispose();
        }

        public IList<Contact> SearchContactsByName(Guid userId, string forename, string surname)
        {
            return _contactRepository.GetAll().Where(x => x.UserId == userId && x.Forename.Contains(forename) && x.Surname.Contains(surname)).ToList();
        }

        public IList<Contact> SearchContactsByEmail(Guid userId, string email)
        {
            return _contactRepository.GetAll().Where(x => x.UserId == userId && x.Email.Contains(email)).ToList();
        }

        public IList<Contact> GetAllByUserId(Guid userId)
        {
            return _contactRepository.GetAll().Where(x => x.UserId == userId).ToList();
        }
    }
}
