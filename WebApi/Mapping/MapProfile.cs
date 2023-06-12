using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Concrete;
using WebApi.Dto;

namespace WebApi.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Category, CategoryWithProductDto>();
            CreateMap<CategoryWithProductDto, Category>();
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<Category, AddCategoryDto>();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<Product, AddProductDto>();
            CreateMap<AddProductDto, Product>();
            CreateMap<WebApi.Dto.ProductListDto, Entities.Dto.ProductListDto>();
            CreateMap<Entities.Dto.ProductListDto, WebApi.Dto.ProductListDto>();
            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<ProductWithCategoryDto, Product>();
            CreateMap<User, GetUserDto>();
            CreateMap<GetUserDto, User>();
        }
    }
}