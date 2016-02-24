using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Domain.Interfaces.Services
{
	public interface ITokenService
	{
		Token GenerateToken(Guid userId);
		bool ValidateToken(Guid tokenId);
		bool Kill(Guid tokenId);
		bool DeleteByUserId(Guid userId);
	}
}
