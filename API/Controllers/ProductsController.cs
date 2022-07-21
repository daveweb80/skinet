using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;

namespace API.Controllers
{
       public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository <Product> productRepo,
                                   IGenericRepository <ProductBrand> productBrandRepo,
                                   IGenericRepository <ProductType> ProductTypeRepo,
                                   IMapper Mapper)
        {
            this._productRepo = productRepo;
            this._productBrandRepo = productBrandRepo;
            _productTypeRepo = ProductTypeRepo;
            _mapper = Mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDtos>>> GetProducts() 
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDtos>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDtos>> GetProduct(int id) 
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.getEntityWithSpec(spec);
            
            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDtos>(product);
        } 

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() 
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        } 
       
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() 
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        } 
    
    }
}

