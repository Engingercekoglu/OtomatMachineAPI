using System;
using System.Collections.Generic;
using System.Text;
using static OtomatMachine.Shared.Enum.Enum;

namespace OtomatMachine.Entity.Dtos
{
 public   class ProductListDTO
    {
        public Status Status { get; set; }

        public int ProductTypeId { get; set; }

    }
}
