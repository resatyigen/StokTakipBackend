using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class UserJwtDto
    {
        public int ID { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}