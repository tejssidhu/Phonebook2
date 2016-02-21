using Phonebook.Data.Context;
using Phonebook.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Phonebook.Domain.Model;
using Phonebook.Domain.Exceptions;

namespace Phonebook.Data.Repositories
{
	public class ContactNumberRepository : IContactNumberRepository
	{
		private readonly PhonebookContext _phonebookContext;

		public ContactNumberRepository(PhonebookContext phonebookContext)
		{
			_phonebookContext = phonebookContext;
		}

		public IQueryable<ContactNumber> GetAll()
		{
			return _phonebookContext.ContactNumbers;
		}

		public ContactNumber Get(Guid id)
		{
			return _phonebookContext.ContactNumbers.FirstOrDefault(cn => cn.Id == id);
		}

		public Guid Create(ContactNumber model)
		{
			model.Id = Guid.NewGuid();

			_phonebookContext.ContactNumbers.Add(model);

			return model.Id;
		}

		public void Update(ContactNumber model)
		{
			var contactNumber = Get(model.Id);

			if (contactNumber != null)
			{
				contactNumber.Description = model.Description;
				contactNumber.TelephoneNumber = model.TelephoneNumber;
			}
		}

		public void Delete(Guid id)
		{
			var contactToDelete = _phonebookContext.ContactNumbers.FirstOrDefault(cn => cn.Id == id);

			if (contactToDelete == null)
				throw new ObjectNotFoundException("Contact");

			_phonebookContext.ContactNumbers.Remove(contactToDelete);
		}

		public void DeleteContactNumbersByContactId(Guid contactId)
		{
			var contactNumbers = _phonebookContext.ContactNumbers.Where(cn => cn.ContactId == contactId).ToList();

			foreach (var contactNumber in contactNumbers)
			{
				_phonebookContext.ContactNumbers.Remove(contactNumber);
			}
		}
	}
}
