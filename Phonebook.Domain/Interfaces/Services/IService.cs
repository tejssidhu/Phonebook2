using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Domain.Interfaces.Services
{
    public interface IService<T> : IDisposable
    {
        IQueryable<T> GetAll();
        T Get(Guid id);
        Guid Create(T model);
        void Update(T model);
        void Delete(Guid id);
    }
}
