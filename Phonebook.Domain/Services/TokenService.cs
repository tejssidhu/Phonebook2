using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Phonebook.Domain.Interfaces.UnitOfWork;

namespace Phonebook.Domain.Services
{
	public class TokenService : ITokenService
	{
		private readonly IUnitOfWork _unitOfWork;

		public TokenService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		//public void Dispose()
		//{
		//	_unitOfWork.Dispose();
		//}

		//public Model.Token GenerateToken(Guid userId)
		//{
		//	string authToken = Guid.NewGuid().ToString();
		//	DateTime issuedOn = DateTime.Now;
		//	DateTime expiredOn = DateTime.Now.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));

		//	var token = new Token
		//	{
		//		UserId = userId,
		//		AuthToken = authToken,
		//		IssuedOn = issuedOn,
		//		ExpiresOn = expiredOn
		//	};

		//	_unitOfWork.TokenRepository.Insert(token);
		//	_unitOfWork.Save();

		//	return token;
		//}

		public Model.Token GenerateToken(Guid userId)
		{
			throw new NotImplementedException();
		}

		public bool ValidateToken(Guid tokenId)
		{
			throw new NotImplementedException();
		}

		public bool Kill(Guid tokenId)
		{
			throw new NotImplementedException();
		}

		public bool DeleteByUserId(Guid userId)
		{
			throw new NotImplementedException();
		}
	}
}
