using System;
using System.Collections.Generic;
using System.Text;

namespace OtomatMachine.Entity.Dtos
{
    public class ReceiptTransactionRequestDTO
    {
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
        public int SugarCount { get; set; }

        public int PaymentTypeId { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
