using System;
using System.Collections.Generic;
using System.Text;

namespace OtomatMachine.Entity.Dtos
{
    public class ReceiptTransactionResponseDTO
    {
        public int ReceiptTransactionId { get; set; }
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public decimal TotalPrice { get; set; }
        public int SugarCount { get; set; }

        public string PaymentTypeName { get; set; }
        public decimal RefundedAmount { get; set; }

    }
}
