using Phonebook.Domain.Model;

namespace Phonebook.Domain.Interfaces.Services
{
    public interface IUserService : IService<User>
    {
        User Authenticate(string username, string password);
    }
}
