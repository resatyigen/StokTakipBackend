using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "ERR_REQUIRED")]
        [MinLength(4, ErrorMessage = "ERR_MIN_LENGTH_4")]
        [MaxLength(20, ErrorMessage = "ERR_MAX_LENGTH_20")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ERR_REQUIRED")]
        [MinLength(4, ErrorMessage = "ERR_MIN_LENGTH_4")]
        [MaxLength(30, ErrorMessage = "ERR_MAX_LENGTH_30")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ERR_REQUIRED")]
        [EmailAddress(ErrorMessage = "ERR_EMAIL_ADDRESS_INVALIDATED")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "ERR_REQUIRED")]
        [MinLength(8, ErrorMessage = "ERR_MIN_LENGTH_8")]
        [MaxLength(30, ErrorMessage = "ERR_MAX_LENGTH_30")]
        public string Password { get; set; } = string.Empty;
    }
}