using System;
using System.Collections.Generic;
using System.Text;

namespace OtomatMachine.Entity.Dtos
{
  public  class ReceiptTransactionListRequestDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ProductId { get; set; }
        public int PaymentTypeId { get; set; }

    }
}
