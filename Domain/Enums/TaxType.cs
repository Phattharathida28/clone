using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum TaxType
    {
        
        //การแก้ไข
        [Description("ไม่รวมภาษี")]
        TaxExclude = 1, //ไม่รวมภาษี

        [Description("รวมภาษี")]
        TaxInclude = 2 //รวมภาษี
    }
}

