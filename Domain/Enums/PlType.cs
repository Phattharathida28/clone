using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum PLType
    {
        //ประเภทแฟ้มราคา , ชุดราคา
        [Description("Standard Price")]
        W = 1,

        [Description("Specific Price")]
        R = 2 
    }
}
