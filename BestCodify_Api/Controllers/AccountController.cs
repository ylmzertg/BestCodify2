using BestCodify.Common;
using BestCodify.DataAccess.Data;
using BestCodify.Models;
using BestCodify_Api.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private readonly ApiSettings _apiSettings;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IOptions<ApiSettings> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _apiSettings = options.Value;
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

                var signInCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);
                var tokenOperations = new JwtSecurityToken(
                    issuer: _apiSettings.ValidIssuer,
                    audience: _apiSettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signInCredentials);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOperations);
                var returnData = new UserDto
                {
                    Name = user.Name,
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNo=user.PhoneNumber,
                    Token = token
                };
                return Ok(new Result<UserDto>(true, ResultConstant.TokenResponseMessage, returnData));
            }
            else
                return Unauthorized(new Result<IActionResult>(false, ResultConstant.InvalidAuthentication));
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Id",user.Id)
            };
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
