using OtomatMachine.Entity.Dtos;
using OtomatMachine.Shared.Utilities.Results.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using static OtomatMachine.Shared.Enum.Enum;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.ReceiptTransaction
{
    public interface IReceiptTransactionService
    {
        Task<IDataResult<ReceiptTransactionResponseDTO>> AddAsync(ReceiptTransactionRequestDTO receiptTransaction);
        Task<IDataResult<List<ReceiptTransactionResponseDTO>>> GetList(ReceiptTransactionListRequestDTO request);
    }
}
