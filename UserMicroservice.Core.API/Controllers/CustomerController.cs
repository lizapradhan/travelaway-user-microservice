using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserMicroservice.Core.API.Interfaces;
using UserMicroservice.Core.API.ViewModels;

namespace UserMicroservice.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomerDetails(UpdateUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var claimsIdentity = User.Identity as ClaimsIdentity;
                    if (claimsIdentity != null)
                    {
                        var emailId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

                        var result = await _customerRepository.UpdateCustomer(user, emailId);
                        if (result != null)
                        {
                            return Ok();
                        }
                    }

                    return Unauthorized(new
                    {
                        message = "User is not authorized"
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

        [HttpGet("details")]
        public async Task<IActionResult> GetCustomerDetails()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    var emailId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

                    var result = await _customerRepository.GetCustumerDetails(emailId);
                    if (result != null)
                    {
                        return Ok(new
                        {
                            firstName = result.FirstName,
                            lastName = result.LastName,
                            emailId = result.EmailId,
                            gender = result.Gender,
                            contactNumber = result.ContactNumber,
                            address = result.Address,
                            dateOfBirth = result.Dob
                        });
                    }
                }

                return Unauthorized(new
                {
                    message = "User is not authorized"
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
