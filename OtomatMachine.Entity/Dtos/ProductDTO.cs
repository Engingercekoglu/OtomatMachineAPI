using System;
using System.Collections.Generic;
using System.Text;

namespace OtomatMachine.Entity.Dtos
{
 public   class ProductDTO
    {
        public string Name { get; set; }
        public int ProductTypeId { get; set; }

        public decimal Price { get; set; }
        public bool IsHotDrink { get; set; }

    }
}
