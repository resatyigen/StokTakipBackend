using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Core.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Validation;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper
            )
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int ID)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var category = await categoryService.GetAsync(ID);

            if (category == null)
            {
                return BadRequest("ERR_CATEGORY_NOT_FOUND");
            }

            if (category.UserID != int.Parse(userId))
            {
                return BadRequest("ERR_CATEGORY_ACCESS_DENIED");
            }

            return Ok(new { status = "SUCCESS", result = category });
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var categoryList = await categoryService.GetAllAsync(int.Parse(userId));

            return Ok(new { status = "SUCCESS", result = categoryList });
        }

        [HttpGet]
        [Route("FilterList")]
        public async Task<IActionResult> GetFilterList([FromQuery] GetCategoryListDto model)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            int skip = model.RowsPerPage * model.Page;
            int take = model.RowsPerPage;
            Order order = model.Order == "desc" ? Order.DESC : Order.ASC;

            var categories = await categoryService.GetAllByFilter(int.Parse(userId), model.CategoryName, order, skip, take);

            return Ok(new { status = "SUCCESS", result = categories });
        }

        [HttpPost]
        [Route("Add")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddCategory([FromForm] AddCategoryDto model)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            string imagePath = "";

            if (model.ImageFile != null)
            {

                var fileName = Path.GetFileName(model.ImageFile.FileName);
                var rondomFileName = Guid.NewGuid() + fileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "images", rondomFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                imagePath = rondomFileName;
            }

            var mappedCategory = mapper.Map<Category>(model);
            mappedCategory.UserID = int.Parse(userId);
            mappedCategory.ImagePath = imagePath;

            var newCategory = await categoryService.AddAsync(mappedCategory);

            return Ok(new { status = "SUCCESS", result = newCategory });
        }

        [HttpPut]
        [Route("Edit")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> EditCategory([FromForm] EditCategoryDto model)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var updatedCategory = await categoryService.GetAsync(model.ID);

            if (updatedCategory == null)
            {
                return BadRequest("ERR_CATEGORY_NOT_FOUND");
            }

            if (updatedCategory.UserID != int.Parse(userId))
            {
                return BadRequest("ERR_CATEGORY_ACCESS_DENIED");
            }

            string imagePath = "";

            if (model.ImageFile != null)
            {
                var fileName = Path.GetFileName(model.ImageFile.FileName);
                var rondomFileName = Guid.NewGuid() + fileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "images", rondomFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                imagePath = rondomFileName;

                updatedCategory.ImagePath = imagePath;
            }

            updatedCategory.CategoryName = model.CategoryName;
            updatedCategory.Color = model.Color;
            updatedCategory.Description = model.Description;

            var upCategory = await categoryService.UpdateAsync(updatedCategory);

            return Ok(new { status = "SUCCESS", result = upCategory });
        }

        [HttpDelete]
        [Route("Delete")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteCategory([FromQuery] int ID)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var deletedCategory = await categoryService.GetAsync(ID);

            if (deletedCategory == null)
            {
                return BadRequest("ERR_CATEGORY_NOT_FOUND");
            }

            if (deletedCategory.UserID != int.Parse(userId))
            {
                return BadRequest("ERR_CATEGORY_ACCESS_DENIED");
            }

            categoryService.Delete(deletedCategory);

            return Ok(new { status = "SUCCESS" });
        }

    }
}