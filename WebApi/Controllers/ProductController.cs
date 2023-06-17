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
        public async Task<IActionResult> Get([FromQuery] int ID)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var product = await productService.GetWithCategoryAsync(ID);

            if (product == null)
            {
                return BadRequest("ERR_PRODUCT_NOT_FOUND");
            }

            if (product.Category.UserID != int.Parse(userId))
            {
                return BadRequest("ERR_PRODUCT_ACCESS_DENIED");
            }

            var mappedProduct = mapper.Map<ProductDto>(product);

            return Ok(new { status = "SUCCESS", result = mappedProduct });
        }


        [HttpGet]
        [Route("ListByCategoryId")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ListByCategoryId([FromQuery] int categoryID)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var productList = await productService.GetAllByUserIDAsync(int.Parse(userId));

            var mappedProductList = mapper.Map<List<ProductDto>>(productList);

            return Ok(mappedProductList);
        }

        [HttpGet]
        [Route("FilterList")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetFilterList([FromQuery] GetProductListDto model)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }


            int skip = model.RowsPerPage * model.Page;
            int take = model.RowsPerPage;
            Order order = model.Order == "desc" ? Order.DESC : Order.ASC;

            var products = await productService.GetAllByFilter(int.Parse(userId), model.CategoryID, model.ProductName, order, skip, take);

            var mappedProductList = mapper.Map<WebApi.Dto.ProductListDto>(products);

            return Ok(new { status = "SUCCESS", result = mappedProductList });
        }


        [HttpPost]
        [Route("Add")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto model)
        {

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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
                return BadRequest("ERR_PRODUCT_ACCESS_DENIED");
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

            var mappedProduct = mapper.Map<Product>(model);
            mappedProduct.ImagePath = imagePath;

            var newProduct = await productService.AddAsync(mappedProduct);

            return Ok(new { status = "SUCCESS", result = newProduct });
        }

        [HttpPut]
        [Route("Edit")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> EditProduct([FromForm] EditProductDto model)
        {

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var updatedProduct = await productService.GetWithCategoryAsync(model.ID);

            if (updatedProduct == null)
            {
                return BadRequest("ERR_PRODUCT_NOT_FOUND");
            }

            if (updatedProduct.Category == null)
            {
                return BadRequest("ERR_CATEGORY_NOT_FOUND");
            }

            if (updatedProduct.Category.UserID != int.Parse(userId))
            {
                return BadRequest("ERR_PRODUCT_ACCESS_DENIED");
            }

            if (updatedProduct.CategoryID != model.CategoryID)
            {
                var productCategory = await categoryService.GetAsync(model.CategoryID);
                if (productCategory == null)
                {
                    return BadRequest("ERR_CATEGORY_NOT_FOUND");
                }

                if (productCategory.UserID != int.Parse(userId))
                {
                    return BadRequest("ERR_CATEGORY_ACCESS_DENIED");
                }
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
                updatedProduct.ImagePath = imagePath;
            }

            updatedProduct.ProductName = model.ProductName;
            updatedProduct.Description = model.Description;
            updatedProduct.ProductUrl = model.ProductUrl;
            updatedProduct.Quantity = model.Quantity;
            updatedProduct.CategoryID = model.CategoryID;

            var upProduct = await productService.UpdateAsync(updatedProduct);

            var mappingProduct = mapper.Map<ProductDto>(updatedProduct);

            return Ok(new { status = "SUCCESS", result = mappingProduct });
        }


        [HttpPut]
        [Route("UpdateQuantity")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateQuantity([FromForm] UpdateQuantityDto model)
        {

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var updatedProduct = await productService.GetWithCategoryAsync(model.ProductID);

            if (updatedProduct == null)
            {
                return BadRequest("ERR_PRODUCT_NOT_FOUND");
            }

            if (updatedProduct.Category == null)
            {
                return BadRequest("ERR_CATEGORY_NOT_FOUND");
            }

            if (updatedProduct.Category.UserID != int.Parse(userId))
            {
                return BadRequest("ERR_PRODUCT_ACCESS_DENIED");
            }

            updatedProduct.Quantity = model.Quantity;

            await productService.UpdateAsync(updatedProduct);

            return Ok(new { status = "SUCCESS" });
        }


        [HttpDelete]
        [Route("Delete")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteProduct([FromQuery] int ID)
        {

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("ERR_USER_ID");
            }

            var deletedProduct = await productService.GetWithCategoryAsync(ID);

            if (deletedProduct == null)
            {
                return BadRequest("ERR_PRODUCT_NOT_FOUND");
            }

            if (deletedProduct.Category.UserID != int.Parse(userId))
            {
                return BadRequest("ERR_PRODUCT_ACCESS_DENIED");
            }

            productService.Delete(deletedProduct);

            return Ok(new { status = "SUCCESS" });
        }

    }
}