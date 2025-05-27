using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.IN
{
    public class InGoods : EntityBase
    {
        public int ItemId { get; set; }
        public string OuCode { get; set; }
        public string ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemNameEng { get; set; }
        public string? IProductType { get; set; }
        public string? Active { get; set; }
        public decimal price { get; set; }
    
    }
}
