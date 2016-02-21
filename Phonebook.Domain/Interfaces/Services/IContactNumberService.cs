﻿using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Domain.Interfaces.Services
{
    public interface IContactNumberService : IService<ContactNumber>
    {
		IQueryable<ContactNumber> GetAllByContactId(Guid contactId);
    }
}
