using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ProductType
    {
        //การแก้ไข
        [Description("บริการ")]
        S = 1, //บริการ

        [Description("สินค้า")]
        F = 2 //สินค้า
    }
}
