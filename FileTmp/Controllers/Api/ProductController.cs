using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileTmp.Models;
using FileTmp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileTmp.Controllers.Api
{
    [Route("Api/[Controller]")]
    [ApiController]

    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpPut("AddProduct")]

        public async Task<bool> AddProduct([FromBody] Product product)
        {
            return _productService.AddProduct(product);

        }

        [AllowAnonymous]
        [HttpGet("GetProductByLetter")]

        public async Task<List<Product>> GetProductByLetter(string letter)
        {
            return _productService.GetProductsByFirstLetter(letter);

        }

        [AllowAnonymous]
        [HttpGet("SeedProducts")]

        public async Task<bool> SeedProducts()
        {
             _productService.SeedProducts();
            return true;

        }
    }
}

