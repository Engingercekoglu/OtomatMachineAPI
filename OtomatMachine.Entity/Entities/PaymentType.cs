using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace OtomatMachine.Entity.Entities
{
    [Table("PaymentTypes")]
    public class PaymentType : BaseEntity
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]

        public bool IsCard { get; set; }
    }
}
