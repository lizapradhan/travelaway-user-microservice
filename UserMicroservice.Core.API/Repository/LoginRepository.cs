using Microsoft.EntityFrameworkCore;
using UserMicroservice.Core.API.Interfaces;
using UserMicroservice.Core.API.Models;

namespace UserMicroservice.Core.API.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly UserDbContext _context;

        public LoginRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> LoginAsync(string username)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x=>x.EmailId == username);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
