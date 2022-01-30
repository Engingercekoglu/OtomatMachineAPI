using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OtomatMachine.Bussiness.Services.ReceiptTransaction;
using OtomatMachine.Entity.Dtos;
using System;
using System.Threading.Tasks;

namespace OtomatMachineAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReceiptTransactionController : ControllerBase
    {
        private readonly ILogger<ReceiptTransactionController> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly IReceiptTransactionService _ReceiptTransactionService;
        public ReceiptTransactionController(IHostingEnvironment environment, IReceiptTransactionService ReceiptTransactionService,ILogger<ReceiptTransactionController> logger)
        {
        this._ReceiptTransactionService = ReceiptTransactionService;
            _environment = environment;
            this._logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> AddReceiptTransaction([FromBody] ReceiptTransactionRequestDTO receiptTransactionDTO)
        {
            _logger.LogInformation("AddReceiptTransaction çalıştı.", receiptTransactionDTO);
            var result = await _ReceiptTransactionService.AddAsync(receiptTransactionDTO);

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> GetReceiptTransactionList([FromBody] ReceiptTransactionListRequestDTO receiptTransactionListRequestDTO)
        {
            _logger.LogInformation("GetReceiptTransactionList çalıştı.", receiptTransactionListRequestDTO);
            var result = await _ReceiptTransactionService.GetList(receiptTransactionListRequestDTO);
            return Ok(result);
        }
    }
}
