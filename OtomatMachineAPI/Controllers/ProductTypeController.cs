using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OtomatMachine.Bussiness.Services.ProductType;
using OtomatMachine.Entity.Dtos;
using System;
using System.Threading.Tasks;

namespace OtomatMachineAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly ILogger<ProductTypeController> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly IProductTypeService _ProductTypeService;
        public ProductTypeController(IHostingEnvironment environment, IProductTypeService ProductTypeService,ILogger<ProductTypeController> logger)
        {
        this._ProductTypeService = ProductTypeService;
            _environment = environment;
            this._logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> AddProductType([FromBody] ProductTypeDTO ProductTypeDTO)
        {
            _logger.LogInformation("AddProductType çalıştı.", ProductTypeDTO);
            var result = await _ProductTypeService.AddAsync(new OtomatMachine.Entity.Entities.ProductType { SlotCount=ProductTypeDTO.SlotCount, CreatedDate=DateTime.Now,Name=ProductTypeDTO.Name,Status=OtomatMachine.Shared.Enum.Enum.Status.Active});

            return Ok(result);
        }
        [HttpPost]       
        public async Task<IActionResult> DeleteProductType([FromBody] DeleteProductTypeDTO ProductTypeDTO)
        {
            _logger.LogInformation("DeleteProductType çalıştı.", ProductTypeDTO);
            var result = await _ProductTypeService.DeleteAsync(ProductTypeDTO.Id);

            return Ok(result);
        }     
        [HttpPost]
        public async Task<IActionResult> GetProductTypeList([FromBody] ProductTypeListDTO ProductTypeDTO)
        {
            _logger.LogInformation("GetProductTypeList çalıştı.", ProductTypeDTO);
            var result = await _ProductTypeService.GetList(ProductTypeDTO.Status);
            return Ok(result);
        }
    }
}
