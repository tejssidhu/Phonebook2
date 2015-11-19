using System;
using System.Collections.Generic;
using System.Linq;
using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;

namespace Phonebook.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Dispose()
        {
            _userRepository.Dispose();
        }

        public IList<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User Get(Guid id)
        {
            return _userRepository.Get(id);
        }

        public Guid Create(User model)
        {
            var user = _userRepository.GetAll().SingleOrDefault(u => u.Username == model.Username);

            if (user != null) throw new ObjectAlreadyExistException("User");

            var id = _userRepository.Create(model);

            return id;
        }

        public void Update(User model)
        {
            if (_userRepository.GetAll().Any(u => u.Username == model.Username && u.Id != model.Id))
            {
                throw new ObjectAlreadyExistException("User", "username");
            }

            _userRepository.Update(model);
        }

        public void Delete(Guid id)
        {
            _userRepository.Delete(id);
        }

        public User Authenticate(string username, string password)
        {
            var user = _userRepository.GetAll().SingleOrDefault(u => u.Username == username);

            if (user == null) throw new ObjectNotFoundException("User");

            if (user.Password != password.Trim())
            {
                throw new InvalidPasswordException();
            }

            return user;
        }

    }
}