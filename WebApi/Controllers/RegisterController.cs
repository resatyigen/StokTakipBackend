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
    public class RegisterController : ControllerBase
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
            if (await userService.CheckUserName(model.UserName))
            {
                return BadRequest("ERR_USERNAME_FOUND");
            }

            if (await userService.CheckEmail(model.Email))
            {
                return BadRequest("ERR_EMAIL_FOUND");
            }

            string passwordHash = hash.CreateHash(model.Password);
            model.Password = passwordHash;

            var mappedUserModel = mapper.Map<User>(model);
            var newUser = await userService.AddAsync(mappedUserModel);

            return Ok(new { status = "SUCCESS", result = newUser });
        }
    }
}