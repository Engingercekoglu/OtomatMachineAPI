using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OtomatMachine.Bussiness.Services.PaymentType;
using OtomatMachine.Entity.Dtos;
using System;
using System.Threading.Tasks;

namespace OtomatMachineAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly ILogger<PaymentTypeController> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly IPaymentTypeService _paymentTypeService;
        public PaymentTypeController(IHostingEnvironment environment, IPaymentTypeService paymentTypeService,ILogger<PaymentTypeController> logger)
        {
           this._paymentTypeService = paymentTypeService;
            _environment = environment;
            this._logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> AddPaymentType([FromBody] PaymentTypeDTO paymentTypeDTO)
        {
            _logger.LogInformation("AddPaymentType çalıştı.", paymentTypeDTO);
            var result = await _paymentTypeService.AddAsync(new OtomatMachine.Entity.Entities.PaymentType { IsCard=paymentTypeDTO.IsCard,CreatedDate=DateTime.Now,Name=paymentTypeDTO.Name,Status=OtomatMachine.Shared.Enum.Enum.Status.Active});

            return Ok(result);
        }
        [HttpPost]       
        public async Task<IActionResult> DeletePaymentType([FromBody] DeletePaymentTypeDTO paymentTypeDTO)
        {
            _logger.LogInformation("DeletePaymentType çalıştı.", paymentTypeDTO);
            var result = await _paymentTypeService.DeleteAsync(paymentTypeDTO.Id);
            
            return Ok(result);
        }     
        [HttpPost]
        public async Task<IActionResult> GetPaymentTypeList([FromBody] PaymentTypeListDTO paymentTypeDTO)
        {
            _logger.LogInformation("GetPaymentTypeList çalıştı.", paymentTypeDTO);
            var result = await _paymentTypeService.GetList(paymentTypeDTO.Status);
            return Ok(result);
        }
    }
}
