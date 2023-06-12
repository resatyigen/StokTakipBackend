using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class GetUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? PhotoPath { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool EmailValidated { get; set; }
    }
}