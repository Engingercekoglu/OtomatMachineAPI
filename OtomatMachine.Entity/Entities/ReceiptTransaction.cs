using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtomatMachine.Entity.Entities
{
    [Table("ReceiptTransactions")]
    public class ReceiptTransaction :BaseEntity
    {
        public ReceiptTransaction()
        {

        }
        public Product Product { get; set; }

        public int ProductId { get; set; }
        public int ProductCount { get; set; }

        public PaymentType PaymentType { get; set; }
        public int PaymentTypeId { get; set; }
        public int SugarCount { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal RefundedAmount { get; set; }


    }

}
