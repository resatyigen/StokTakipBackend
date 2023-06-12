using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "ERR_REQUIRED")]
        [MinLength(8, ErrorMessage = "ERR_MIN_LENGTH_8")]
        [MaxLength(30, ErrorMessage = "ERR_MAX_LENGTH_30")]
        public string Password { get; set; } = string.Empty;
    }
}