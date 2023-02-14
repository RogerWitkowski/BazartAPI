using Bazart.Models.Dto.EmailDto;
using Bazart.Models.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;
using Bazart.EmailService.EmailService.IEmailService;
using Bazart.Repository.Repository.IRepository;
using Bazart.ErrorHandlingMiddleware.Exceptions;

namespace Bazart.Controllers.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;

        public AccountController(IAccountRepository accountRepository, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
        }

        private async Task<bool> GenerateConfirmation(string token, string userId)
        {
            var userEmailIsUserExist = await _accountRepository.IsUserExist(userId);
            var emailBody = "Please click on this link to confirm your email address: #URL# ";

            var confirmationLink = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmailAddress", "Account",
                new ConfirmEmailAddressDto() { Token = token, UserId = userId });

            var encodedEmailBody = emailBody.Replace("#URL#",
                confirmationLink);

            var result = await _emailService.SendEmailAsync(encodedEmailBody, userEmailIsUserExist);

            return result;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            var newUser = await _accountRepository.RegisterUserAsync(registerUserDto);
            if (newUser.Value == null)
            {
                throw new BadRequestException("NOT GOOD");
            }

            var result = await GenerateConfirmation(newUser.Value.Token, newUser.Value.UserId);

            if (result)
            {
                return Ok("Please verify your email, through the verification email we have just send");
            }

            return Ok("Please request an email verification link.");
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _accountRepository.LoginUserAsync(loginUserDto);

            if (user.Value is not null)
            {
                var result = await GenerateConfirmation(user.Value.Token, user.Value.UserId);

                if (result)
                {
                    return Ok("Please verify your email, through the verification email we have just send");
                }

                return Ok("Please request an email verification link.");
            }

            var jwtToken = await _accountRepository.CreateJwtToken(loginUserDto);
            return Ok($"Successfully logged in! Token: {jwtToken}");
        }

        [HttpGet("confirm-email-address")]
        public async Task<ActionResult> ConfirmEmailAddress(string userId, string token)
        {
            var result = await _accountRepository.ConfirmEmailAddressAsync(userId, token);

            return result;
        }

        //[HttpGet("AuthApp")]
        //public class Task<ActionResult> private AuthentcatorWithMFASetup()
    }
}