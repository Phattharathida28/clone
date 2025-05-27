using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.QORT01.DTO
{
    public class DetailDTO
    {
        public int PLId { get; set; } //key qo master
        public string? PLTypeCode { get; set; } //รหัส ประเภทแฟ้มราคา ชุดราคา
        public string? PLTypeName { get; set; } //ประเภทแฟ้มราคา ชุดราคา
        public string? PLCode { get; set; } //รหัส แฟ้ม
        public string? PLNameTH { get; set; } //ชื่อแฟ้ม TH
        public string? PLNameEN { get; set; }//ชื่อแฟ้ม EN
        public string? CurrencyCode { get; set; } //รหัส สกุลเงิน
        public string? CurrencyName { get; set; } //ชื่อสกุลเงิน
        public string? TaxTypeCode { get; set; } //รหัส ประเภทภาษี
        public string? TaxTypeName { get; set; } //ชื่อประเภทภาษี
        public string? Remark { get; set; } //หมายเหตุ
        public List<DetailGoodsServiceDTO> DetailItems { get; set; }
    }

    public class DetailGoodsServiceDTO
    {
        public int PLDetId { get; set; }
        public EditType? EditTypeCode { get; set; }
        public string? EditTypeName { get; set; } //การแก้ไข

        public decimal? ItemId { get; set; }
        public string? ItemName { get; set; } //ชื่อสินค้า และบริการ
        public decimal? UnitId { get; set; }
        public string? UnitCode { get; set; }
        public string? UnitName { get; set; } //หน่วย
        public decimal? StandardPrice { get; set; } //ราคาขาย
        public decimal? Price { get; set; }//ปรับราคาขาย
    }
    public class QoPriceDetailDTO
    {
        public int? PlDetId { get; set; }
        public string OuCode { get; set; }
        public decimal PlId { get; set; }
        public decimal ItemId { get; set; }
        public decimal UnitId { get; set; }
        public string? ProductType { get; set; }
        public string? EditType { get; set; }
        public decimal? StandardPrice { get; set; } //ราคาขาย
        public decimal? Price { get; set; }//ปรับราคาขาย
    }
}
