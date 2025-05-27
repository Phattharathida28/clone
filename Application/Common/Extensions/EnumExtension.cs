using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extensions
{
    public static class EnumExtension
    {
        // Extension method สำหรับ Enum ที่ช่วยให้สามารถดึงคำอธิบาย (Description) ของค่า Enum
        public static string GetDescription(this Enum GenericEnum)
        {
            // ตรวจสอบว่า GenericEnum มีค่าเป็น null หรือไม่
            if (GenericEnum == null)
            {
                return string.Empty;  // ถ้าเป็น null จะคืนค่าเป็น string ว่าง
            }

            // ดึงประเภท (Type) ของ GenericEnum
            Type genericEnumType = GenericEnum.GetType();

            // ดึงข้อมูลสมาชิก (Member) ของ Enum ที่ใช้ในขณะนี้
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());

            // ถ้ามี Member และสมาชิกดังกล่าวไม่เป็น null หรือ empty
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                // ดึง attribute ที่เป็น DescriptionAttribute ถ้ามีการกำหนดไว้
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

                // ถ้ามี DescriptionAttribute
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    // คืนค่าคำอธิบายจาก DescriptionAttribute
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }

            // ถ้าไม่พบ DescriptionAttribute, คืนค่าชื่อของ Enum นั้นๆ (เป็น string)
            return GenericEnum.ToString();
        }
    }
}