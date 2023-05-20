using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Entities.Concrete;
using AutoMapper;
using WebApi.Dto;
using WebApi.Validation;
using WebApi.Hash;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {

        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IHash hash;


        public RegisterController(
            IUserService userService,
            IMapper mapper,
            IHash hash
            )
        {
            this.userService = userService;
            this.mapper = mapper;
            this.hash = hash;
        }

        [HttpPost]
        [Route("RegisterUser")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser(RegisterUserDto model)
        {
            // Burada kullanıcı adı ve email kontrolü yapılacak
            string passwordHash = hash.CreateHash(model.Password);
            model.Password = passwordHash;

            var mappedUserModel = mapper.Map<User>(model);
            var newUser = await userService.AddAsync(mappedUserModel);

            Console.WriteLine($"UserName : {model.UserName}");
            Console.WriteLine($"FullName : {model.FullName}");
            Console.WriteLine($"Email : {model.Email}");
            Console.WriteLine($"Password : {model.Password}");

            return Ok(new { status = "SUCCESS", result = newUser });
        }
    }
}