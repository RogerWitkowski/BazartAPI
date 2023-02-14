using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bazart.Models.Dto.UserDto;
using FluentValidation;

namespace Bazart.Models.Dto.Validators
{
    internal class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.ConfirmEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6).MaximumLength(20);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}