using UserMicroservice.Core.API.Models;
using UserMicroservice.Core.API.ViewModels;

namespace UserMicroservice.Core.API.Interfaces
{
    public interface ICustomerRepository
    {
        Task<User> UpdateCustomer(UpdateUser user, string emailId);
        Task<User> GetCustumerDetails(string emailId);
    }
}
