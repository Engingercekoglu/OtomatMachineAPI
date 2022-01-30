using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OtomatMachine.Bussiness.Services.Product;
using OtomatMachine.Entity.Dtos;
using System;
using System.Threading.Tasks;

namespace OtomatMachineAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly IProductService _ProductService;
        public ProductController(IHostingEnvironment environment, IProductService ProductService,ILogger<ProductController> logger)
        {
        this._ProductService = ProductService;
            _environment = environment;
            this._logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO ProductDTO)
        {
            _logger.LogInformation("AddProduct çalıştı.", ProductDTO);
            var result = await _ProductService.AddAsync(new OtomatMachine.Entity.Entities.Product { ProductTypeId=ProductDTO.ProductTypeId,IsHotDrink=ProductDTO.IsHotDrink,Price=ProductDTO.Price, CreatedDate=DateTime.Now,Name=ProductDTO.Name,Status=OtomatMachine.Shared.Enum.Enum.Status.Active});

            return Ok(result);
        }
        [HttpPost]       
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductDTO ProductDTO)
        {
            _logger.LogInformation("DeleteProduct çalıştı.", ProductDTO);
            var result = await _ProductService.DeleteAsync(ProductDTO.Id);

            return Ok(result);
        }     
        [HttpPost]
        public async Task<IActionResult> GetProductList([FromBody] ProductListDTO ProductDTO)
        {
            _logger.LogInformation("GetProductList çalıştı.", ProductDTO);
            var result = await _ProductService.GetList(ProductDTO);
            return Ok(result);
        }
    }
}
