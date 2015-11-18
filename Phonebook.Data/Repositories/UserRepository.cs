using System;
using System.Collections.Generic;
using System.Linq;
using Phonebook.Data.Context;
using Phonebook.Data.Properties;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Model;

namespace Phonebook.Data.Repositories
{
    public class UserRepository : IRepository<User> 
    {
        private readonly PhonebookEntities _phonebookEntities;

        public UserRepository()
        {
            _phonebookEntities = new PhonebookEntities(Settings.Default.FilePaths, Settings.Default.UserFile);
        }

        public void Dispose()
        {
            _phonebookEntities.Dispose();
        }

        public IList<User> GetAll()
        {
            return _phonebookEntities.Users;
        }

        public User Get(Guid id)
        {
            return _phonebookEntities.Users.FirstOrDefault(u => u.Id == id);
        }

        public Guid Create(User model)
        {
            model.Id = Guid.NewGuid();

            _phonebookEntities.Users.Add(model);

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
        }

        public void Delete(Guid id)
        {
            _phonebookEntities.Users.Remove(_phonebookEntities.Users.FirstOrDefault(u => u.Id == id));
        }

        public void SaveChanges()
        {
            _phonebookEntities.SaveChanges();
        }
    }
}
