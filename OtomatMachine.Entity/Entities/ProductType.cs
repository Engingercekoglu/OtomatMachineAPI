using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtomatMachine.Entity.Entities
{
    [Table("ProductTypes")]
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }

        public int SlotCount { get; set; }
      
    }
}
