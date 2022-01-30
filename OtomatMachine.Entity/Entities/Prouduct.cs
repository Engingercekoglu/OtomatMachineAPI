using OtomatMachine.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OtomatMachine.Entity.Entities
{
    [Table("Products")]

    public class Product : BaseEntity
    {
        public Product()
        {

        }
        public string Name { get; set; }

        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Price { get; set; }
        public bool IsHotDrink { get; set; }
    }
}
