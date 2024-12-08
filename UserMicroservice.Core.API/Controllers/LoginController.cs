using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserMicroservice.Core.API.Helper;
using UserMicroservice.Core.API.Interfaces;
using UserMicroservice.Core.API.ViewModels;

namespace UserMicroservice.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        private readonly IConfiguration _configuration;
        public LoginController(ILoginRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDetails = await _loginRepository.LoginAsync(user.UserName);
                    if (userDetails != null)
                    {
                        if(PasswordHelper.VerifyPassword(user.Password, userDetails.Password))
                        {
                            return Ok(new
                            {
                                token = GenerateJwtToken(user.UserName),
                                firstName = userDetails.FirstName,
                                lastName = userDetails.LastName,
                                emailId = userDetails.EmailId,
                                gender = userDetails.Gender,
                                contactNumber = userDetails.ContactNumber,
                                address = userDetails.Address,
                                dateOfBirth = userDetails.Dob
                            });
                        }
                        return BadRequest(new { message = "Incorrect password" });
                    }
                    else
                    {
                        return NotFound(new { message = "User does not exist. " });
                    }
                }

                return BadRequest(new
                {
                    success = false,
                    errors = ModelState
                        .Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage),
                        message="Validation failed"
                });
            }
            catch (Exception ex)
            {
                //Can be logged
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while processing your request. Please try again!!",
                    details = ex.Message
                });
            }
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
