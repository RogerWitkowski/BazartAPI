using System.ComponentModel.DataAnnotations;

namespace Bazart.Models.Dto.UserDto
{
    public class LoginUserDto
    {
        [Required]
        [DataType(dataType: DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(dataType: DataType.Password)]
        public string Password { get; set; }
    }
}