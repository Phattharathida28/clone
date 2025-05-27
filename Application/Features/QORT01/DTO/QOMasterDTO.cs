using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.QORT01.DTO
{
    public class QOMasterDTO
    {
        public int PLId { get; set; } //key
        public string? PLTypeCode { get; set; } //รหัส ประเภทแฟ้มราคา ชุดราคา
        public string? PLTypeName { get; set; } //ประเภทแฟ้มราคา ชุดราคา
        public string? PLCode { get; set; } //รหัส แฟ้ม
        public string? PLName { get; set; } //ชื่อแฟ้ม
        public string? CuurencyCode { get; set; } //รหัส สกุลเงิน
        public string? CurrencyName { get; set; } //ชื่อสกุลเงิน
        public string? TaxTypeCode { get; set; } //รหัส ประเภทภาษี
        public string? TaxTypeName { get; set; } //ชื่อประเภทภาษี

    }
}
