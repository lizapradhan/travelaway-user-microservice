using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Core.API.Helper;
using UserMicroservice.Core.API.Interfaces;
using UserMicroservice.Core.API.Models;
using UserMicroservice.Core.API.ViewModels;

namespace UserMicroservice.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationRepository _registrationRepo;

        private readonly IMapper _mapper;
        public RegistrationController(IMapper mapper, IRegistrationRepository registrationRepo) 
        {
            this._mapper = mapper;
            this._registrationRepo = registrationRepo;
        } 

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _mapper.Map<User>(registerUser);
                    user.Password = PasswordHelper.HashPassword(user.Password);
                    var result = await _registrationRepo.RegisterUser(user);
                    if (result)
                    {
                        return Ok();
                    }
                    return Conflict(new
                    {
                        message="User already exists"
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    errors = ModelState
                        .Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage),
                    message = "Validation failed"
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

    }
}
