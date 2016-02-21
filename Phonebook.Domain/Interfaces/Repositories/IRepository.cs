using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
		IQueryable<TEntity> GetAll();
		TEntity Get(Guid id);
		Guid Create(TEntity model);
		void Update(TEntity model);
        void Delete(Guid id);
    }
}
