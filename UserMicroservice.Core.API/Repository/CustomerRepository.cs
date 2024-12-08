using Microsoft.EntityFrameworkCore;
using UserMicroservice.Core.API.Interfaces;
using UserMicroservice.Core.API.Models;
using UserMicroservice.Core.API.ViewModels;

namespace UserMicroservice.Core.API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly UserDbContext _context;

        public CustomerRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> UpdateCustomer(UpdateUser user, string emailId)
        {
            try
            {
                var userFound = await _context.Users.FirstOrDefaultAsync(x => x.EmailId.ToLower().Equals(emailId.ToLower()));
                if (userFound == null)
                {
                    return null;
                }

                userFound.FirstName = user.FirstName;
                userFound.LastName = user.LastName;
                userFound.Gender = (int)user.Gender;
                userFound.Address = user.Address;
                userFound.ContactNumber = user.ContactNumber;
                userFound.Dob = user.Dob;

                _context.Users.Update(userFound);
                await _context.SaveChangesAsync();
                return userFound;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetCustumerDetails(string emailId)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.EmailId.ToLower().Equals(emailId.ToLower()));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
