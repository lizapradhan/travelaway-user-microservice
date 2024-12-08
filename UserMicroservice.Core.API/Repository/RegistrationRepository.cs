using Microsoft.EntityFrameworkCore;
using UserMicroservice.Core.API.Interfaces;
using UserMicroservice.Core.API.Models;

namespace UserMicroservice.Core.API.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly UserDbContext _context;

        public RegistrationRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUser(User user)
        {
            try
            {
                var isUserFound = await _context.Users.AnyAsync(x => x.EmailId.ToLower().Equals(user.EmailId.ToLower()));

                if (!isUserFound)
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
