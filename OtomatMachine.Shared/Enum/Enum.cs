using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OtomatMachine.Shared.Enum
{
 public   class Enum
    {

        public enum Status
        {
            [Description("Hepsi")]

            All = 0,
            [Description("Aktif")]

            Active = 1,
            [Description("Pasif")]
            Passive = 2

        }

       
    }
}
