using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class AddProductDto
    {
        [Required(ErrorMessage = "ERR_REQUIRED")]
        public int CategoryID { get; set; }

        [MinLength(3, ErrorMessage = "ERR_MIN_LENGTH_3")]
        [MaxLength(200, ErrorMessage = "ERR_MAX_LENGTH_200")]
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "ERR_MAX_LENGTH_500")]
        public string? Description { get; set; }

        [MaxLength(350, ErrorMessage = "ERR_MAX_LENGTH_350")]
        public string? ProductUrl { get; set; }

        [Required(ErrorMessage = "ERR_REQUIRED")]
        public int Quantity { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}