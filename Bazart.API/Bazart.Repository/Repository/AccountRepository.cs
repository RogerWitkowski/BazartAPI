using AutoMapper;
using Bazart.ErrorHandlingMiddleware.Exceptions;
using Bazart.Models.Dto.EmailDto;
using Bazart.Models.Dto.UserDto;
using Bazart.Models.Model;
using Bazart.Repository.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bazart.Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountRepository(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ActionResult<ConfirmEmailAddressDto>> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            if ((registerUserDto.Password == registerUserDto.ConfirmPassword) && (registerUserDto.Email == registerUserDto.ConfirmEmail))
            {
                User newUser = _mapper.Map<User>(registerUserDto);

                var createdUser = _userManager.CreateAsync(newUser, newUser.PasswordHash).GetAwaiter().GetResult();

                if (createdUser.Succeeded)
                {
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
                return new NoContentResult();
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
                    var token = await GenerateConfirmationToken(isUserExist);
                    return token;
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
    }
}