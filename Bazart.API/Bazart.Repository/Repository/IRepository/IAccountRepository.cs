using Bazart.Models.Dto.EmailDto;
using Bazart.Models.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;

namespace Bazart.Repository.Repository.IRepository
{
    public interface IAccountRepository
    {
        public Task<ActionResult<ConfirmEmailAddressDto>> RegisterUserAsync(RegisterUserDto registerUserDto);

        public Task<ActionResult<ConfirmEmailAddressDto>> LoginUserAsync(LoginUserDto loginUserDto);

        public Task<ActionResult> ConfirmEmailAddressAsync(string userId, string token);

        public Task<string> IsUserExist(string userId);

        public Task<string> CreateJwtToken(LoginUserDto loginUserDto);

        public Task AuthWithMFA();
    }
}