using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class UpdateQuantityDto
    {
        [Required(ErrorMessage = "ERR_REQUIRED")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "ERR_REQUIRED")]
        public int Quantity { get; set; }
    }
}