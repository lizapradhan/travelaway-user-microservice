using UserMicroservice.Core.API.Models;

namespace UserMicroservice.Core.API.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<bool> RegisterUser(User user);
    }
}
