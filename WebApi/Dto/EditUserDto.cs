using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class EditUserDto
    {
        [Required(ErrorMessage = "ERR_REQUIRED")]
        [MinLength(3, ErrorMessage = "ERR_MIN_LENGTH_3")]
        [MaxLength(250, ErrorMessage = "ERR_MAX_LENGTH_250")]
        public string FullName { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
    }
}