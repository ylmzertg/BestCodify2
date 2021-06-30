using BestCodify.Common;
using BestCodify.DataAccess.Data;
using BestCodify.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCodify_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserRequestDto userRequestDto)
        {
            if (userRequestDto == null || !ModelState.IsValid)
                return BadRequest();
            var user = new AppUser
            {
                UserName = userRequestDto.Email,
                Email = userRequestDto.Email,
                Name = userRequestDto.Name,
                PhoneNumber = userRequestDto.PhoneNo,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, userRequestDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                //return BadRequest(new RegistrationResponseDto
                //{
                //    Errors = errors,
                //    IsRegisterationSuccessful = false
                //});
                return BadRequest(new Result<IEnumerable<string>>(false, ResultConstant.IdNotNull, errors));
            }
            var roleResult = await _userManager.AddToRoleAsync(user, ResultConstant.Role_Customer);
            if (!roleResult.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                //return BadRequest(new RegistrationResponseDto
                //{
                //    Errors = errors,
                //    IsRegisterationSuccessful = false
                //});
                return BadRequest(new Result<IEnumerable<string>>(false, ResultConstant.IdNotNull, errors));
            }
            return StatusCode(201);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationDto authenticationDto)
        {
            var result = await _signInManager.PasswordSignInAsync(authenticationDto.UserName, authenticationDto.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(authenticationDto.UserName);
                if (user == null)
                    return Unauthorized(new Result<IActionResult>(false, ResultConstant.InvalidAuthentication));

            }
        }
    }
}
