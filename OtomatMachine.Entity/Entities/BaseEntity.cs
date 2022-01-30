using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using static OtomatMachine.Shared.Enum.Enum;

namespace OtomatMachine.Entity.Entities
{
   public class BaseEntity
    {
        [DataMember]

        public int Id { get; set; }
        [DataMember]

        public DateTime CreatedDate { get; set; }
        [DataMember]

        public Status Status { get; set; }
    }
}
