using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Validation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IMapper mapper
        )
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }


        [HttpGet]
        [Route("ListByCategoryId")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ListByCategoryId([FromQuery] int categoryID)
        {
            string? userId = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var checkCategory = await categoryService.GetAsync(categoryID);

            if (checkCategory == null)
            {
                return BadRequest("ERR_CATEGORY_NOT_FOUND");
            }

            if (checkCategory.UserID != int.Parse(userId))
            {
                return BadRequest("ERR_CATEGORY_ACCESS_DENIED");
            }

            var productList = await productService.GetAllByCategoryIDAsync(categoryID);

            var mappedProductList = mapper.Map<List<ProductDto>>(productList);

            return Ok(mappedProductList);
        }


        [HttpGet]
        [Route("List")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> List()
        {
            string? userId = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var productList = await productService.GetAllByUserIDAsync(int.Parse(userId));

            var mappedProductList = mapper.Map<List<ProductDto>>(productList);

            return Ok(mappedProductList);
        }


        [HttpPost]
        [Route("Add")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto model)
        {

            string? userId = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var checkCategory = await categoryService.GetAsync(model.CategoryID);

            if (checkCategory == null)
            {
                return BadRequest("ERR_CATEGORY_NOT_FOUND");
            }

            if (checkCategory.UserID != int.Parse(userId))
            {
                return BadRequest("ERR_CATEGORY_ACCESS_DENIED");
            }

            string imagePath = "";

            if (model.ImageFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images", model.ImageFile.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                imagePath = model.ImageFile.FileName;
            }

            var mappedProduct = mapper.Map<Product>(model);
            mappedProduct.ImagePath = imagePath;

            var newProduct = await productService.AddAsync(mappedProduct);

            return Ok(new { status = "SUCCESS", result = newProduct });
        }

    }
}