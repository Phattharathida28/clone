using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.QO
{
    public class QoPriceDetail : EntityBase
    {
        public int PlDetId { get; set; }
        public string OuCode { get; set; }
        public int PlId { get; set; }
        public decimal ItemId { get; set; }
        public decimal UnitId { get; set; }
        public string ProductType { get; set; }
        public decimal? PriceStandard { get; set; } //ราคาขาย
        public decimal? Price { get; set;  } //ปรับราคาขาย
        public int? EditType { get; set; }

    }
}
