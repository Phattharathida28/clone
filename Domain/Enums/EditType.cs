using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum EditType
    {
        //การแก้ไข
        [Description("คงเดิม")] //การใส่ Description เอาไว้บนตัวแปรที่เรากำหนดเพื่อ เป็นเหมือนการสร้างคำอธิบายว่าตัวแปรนั้นสื่อถึงอะไร
        UnChanged = 1, // ค่าคงที่ or คงเดิม

        [Description("ปรับราคา")]
        PriceChange = 2, //ปรับราคา

        [Description("พิมพ์")]
        AddItem = 3
    }
}