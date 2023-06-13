using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class AddCategoryDto
    {

        [Required(ErrorMessage = "ERR_REQUIRED")]
        [MinLength(3, ErrorMessage = "ERR_MIN_LENGTH_3")]
        [MaxLength(300, ErrorMessage = "ERR_MAX_LENGTH_300")]
        public string CategoryName { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "ERR_MAX_LENGTH_50")]
        public string? Color { get; set; }

        [MaxLength(500, ErrorMessage = "ERR_MAX_LENGTH_500")]
        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}