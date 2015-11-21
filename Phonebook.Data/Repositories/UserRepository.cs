using System;
using System.Collections.Generic;
using System.Linq;
using Phonebook.Data.Context;
using Phonebook.Data.Properties;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Model;

namespace Phonebook.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PhonebookContext _phonebookContext;

        public UserRepository(Configuration config)
        {
            _phonebookContext = new PhonebookContext(config);
        }

        public void Dispose()
        {
            _phonebookContext.Dispose();
        }

        public IList<User> GetAll()
        {
            return _phonebookContext.Users;
        }

        public User Get(Guid id)
        {
            return _phonebookContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public Guid Create(User model)
        {
            model.Id = Guid.NewGuid();

            _phonebookContext.Users.Add(model);

            _phonebookContext.SaveUserChanges();

            return model.Id;
        }

        public void Update(User model)
        {
            var user = Get(model.Id);

            if (user != null)
            {
                user.Username = model.Username;
                user.Password = model.Password;
            }

            _phonebookContext.SaveUserChanges();
        }

        public void Delete(Guid id)
        {
            _phonebookContext.Users.Remove(_phonebookContext.Users.FirstOrDefault(u => u.Id == id));

            _phonebookContext.SaveUserChanges();
        }

    }
}
