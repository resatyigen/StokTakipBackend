using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Entities.Concrete;
using AutoMapper;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IMapper mapper;


        public HomeController(
            IUserService userService,
            ICategoryService categoryService,
            IProductService productService,
            IMapper mapper
            )
        {
            this.userService = userService;
            this.categoryService = categoryService;
            this.productService = productService;
            this.mapper = mapper;
        }

        // [HttpGet(Name = "AddUser")]
        // public async Task<IActionResult> AddUser()
        // {
        //     await userService.AddAsync(new User()
        //     {
        //         UserName = "resatyigen",
        //         Password = "123456",
        //         FullName = "Reşat Yiğen",
        //         Email = "resatyigen@gmail.com"
        //     });

        //     return Ok("İşlem Tamadır");
        // }


        // [HttpGet(Name = "AddCategory")]
        // [Route("AddCategory")]
        // public async Task<IActionResult> AddCategory()
        // {
        //     await categoryService.AddAsync(new Category()
        //     {
        //         UserID = 1,
        //         CategoryName = "Kondansatörler"
        //     });

        //     return Ok("İşlem Tamadır Kategori");
        // }


        // [HttpGet(Name = "AddProduct")]
        // [Route("AddProduct")]
        // public async Task<IActionResult> AddProduct()
        // {
        //     await productService.AddAsync(new Product()
        //     {
        //         CategoryID = 2,
        //         ProductName = "100 uf 10V",
        //         Quantity = 250
        //     });

        //     return Ok("İşlem Tamadır Product");
        // }


        // [HttpGet(Name = "ProductList")]
        // [Route("ProductList")]
        // public async Task<IActionResult> ProductList()
        // {
        //     List<Product> productList = await productService.GetAllByUserIDAsync(1);

        //     return Ok(productList);
        // }


        // [HttpGet(Name = "CategoryList")]
        // [Route("CategoryList")]
        // public async Task<IActionResult> CategoryList()
        // {
        //     var categoryList = await categoryService.GetAllWithProductAsync(1);
        //     var mappedCategoryList = mapper.Map<IEnumerable<CategoryWithProductDto>>(categoryList);
        //     return Ok(mappedCategoryList);
        // }
    }
}