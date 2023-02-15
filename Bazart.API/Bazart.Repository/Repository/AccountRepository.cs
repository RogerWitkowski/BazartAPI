using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Bazart.AuthenticationSettings.Settings;
using Bazart.DataAccess.DataAccess;
using Bazart.ErrorHandlingMiddleware.Exceptions;
using Bazart.Models.Dto.EmailDto;
using Bazart.Models.Dto.UserDto;
using Bazart.Models.Model;
using Bazart.Repository.Repository.IRepository;
using Bazart.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Bazart.Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AuthenticationSettingsModel _authenticationSettings;
        private readonly BazartDbContext _dbContext;

        public AccountRepository(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, AuthenticationSettingsModel authenticationSettings, BazartDbContext dbContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationSettings = authenticationSettings;
            _dbContext = dbContext;
        }

        public async Task<ActionResult<ConfirmEmailAddressDto>> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            if ((registerUserDto.Password == registerUserDto.ConfirmPassword) && (registerUserDto.Email == registerUserDto.ConfirmEmail))
            {
                User newUser = _mapper.Map<User>(registerUserDto);

                var createdUser = _userManager.CreateAsync(newUser, newUser.PasswordHash).GetAwaiter().GetResult();

                if (createdUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, Roles.Customer);
                    var token = await GenerateConfirmationToken(newUser);
                    return token;
                }
            }

            throw new NotFoundException("Wrong password or email.");
        }

        public async Task<ActionResult<ConfirmEmailAddressDto>> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUserDto.Email, loginUserDto.Password, true, true);

            if (result.Succeeded)
            {
                return new OkObjectResult("ok");
            }
            else
            {
                if (result.IsLockedOut)
                {
                    throw new Exception("You are locked out.");
                }
            }
            if (result.IsNotAllowed)
            {
                var isUserExist = await _userManager.FindByEmailAsync(loginUserDto.Email);
                if (isUserExist != null)
                {
                    var token = GenerateConfirmationToken(isUserExist);
                    return token.Result;
                }
            }

            throw new BadRequestException("Login failed.");
        }

        public async Task<ActionResult> ConfirmEmailAddressAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new BadRequestException("Failed to validate email.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            var status = result.Succeeded
                ? "Thank you for confirm your email."
                : "Your email is not confirmed, please try again confirm it later.";

            return new OkObjectResult(status);
        }

        public async Task<string> IsUserExist(string userId)
        {
            var isUserExist = await _userManager.FindByIdAsync(userId);
            return isUserExist.Email;
        }

        private async Task<ConfirmEmailAddressDto> GenerateConfirmationToken(User user)
        {
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return new ConfirmEmailAddressDto() { Token = confirmationToken, UserId = user.Id };
        }

        public async Task AuthWithMFA()
        {
            //User user = await _userManager.GetUserAsync()
            //_userManager.GetAuthenticatorKeyAsync()
            throw new Exception("fdfs");
        }

        public async Task<string> CreateJwtToken(LoginUserDto loginUserDto)
        {
            var user = await _userManager
                .Users
                .FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);

            var userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(u => u.UserId == user.Id);

            if (user == null)
            {
                throw new BadRequestException("Invalid email or password.");
            }

            var isPwCorrect = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password);

            if (isPwCorrect == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{userRole.RoleId}"),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
                new Claim("Nationality", user.Nationality)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireMinutes);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtAudience,
                claims,
                expires: expiresAt,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}