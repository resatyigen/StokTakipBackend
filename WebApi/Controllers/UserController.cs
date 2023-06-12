using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Hash;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IHash hash;

        public UserController(
            IUserService userService,
            IMapper mapper,
            IHash hash
            )
        {
            this.userService = userService;
            this.mapper = mapper;
            this.hash = hash;
        }

        [HttpGet]
        [Route("UserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var user = await userService.GetAsync(int.Parse(userId));

            if (user == null)
            {
                return BadRequest("ERR_USER_NOT_FOUND");
            }

            var mappedUser = mapper.Map<GetUserDto>(user);

            return Ok(new { status = "SUCCESS", result = mappedUser });
        }

        [HttpPut]
        [Route("EditUserInfo")]
        public async Task<IActionResult> EditUserInfo([FromForm] EditUserDto model)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var user = await userService.GetAsync(int.Parse(userId));

            if (user == null)
            {
                return BadRequest("ERR_USER_NOT_FOUND");
            }

            if (model.ImageFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images", model.ImageFile.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                user.PhotoPath = model.ImageFile.FileName;
            }

            user.FullName = model.FullName;

            var updatedUser = await userService.UpdateAsync(user);

            var mappedUpdatedUser = mapper.Map<GetUserDto>(updatedUser);

            return Ok(new { status = "SUCCESS", result = mappedUpdatedUser });
        }

        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordDto model)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var user = await userService.GetAsync(int.Parse(userId));

            if (user == null)
            {
                return BadRequest("ERR_USER_NOT_FOUND");
            }

            string passwordHash = hash.CreateHash(model.Password);

            user.Password = passwordHash;

            await userService.UpdateAsync(user);

            return Ok(new { status = "SUCCESS" });
        }
    }
}