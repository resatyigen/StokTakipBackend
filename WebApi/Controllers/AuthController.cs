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
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using WebApi.Helpers;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IHash hash;
        private readonly AppSettings appSettings;

        public AuthController(
            IUserService userService,
            IMapper mapper,
            IHash hash,
            IOptions<AppSettings> appSettings
            )
        {
            this.userService = userService;
            this.mapper = mapper;
            this.hash = hash;
            this.appSettings = appSettings.Value;
        }


        [HttpPost]
        [Route("Login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Login(LoginDto model)
        {
            Console.WriteLine($"UserName : {model.UserName}");
            Console.WriteLine($"Password : {model.Password}");

            string hashPassword = hash.CreateHash(model.Password);
            var user = await userService.GetUserByUsernameAndPassword(model.UserName, hashPassword);

            Console.WriteLine("User : " + user.Email);

            Console.WriteLine("Secret : " + appSettings.Secret);

            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                string token = tokenHandler.WriteToken(securityToken);

                return Ok(new { status = "SUCCESS", token = token });
            }
            else
            {
                return BadRequest("USER_NOT_FOUND");
            }
        }
    }
}