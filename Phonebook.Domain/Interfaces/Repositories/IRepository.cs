using System;
using System.Collections.Generic;

namespace Phonebook.Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        IList<T> GetAll();
        T Get(Guid id);
        Guid Create(T model);
        void Update(T model);
        void Delete(Guid id);
    }
}
