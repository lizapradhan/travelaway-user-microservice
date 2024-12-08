using UserMicroservice.Core.API.Models;

namespace UserMicroservice.Core.API.Interfaces
{
    public interface ILoginRepository
    {
        Task<User> LoginAsync(string username);
    }
}
