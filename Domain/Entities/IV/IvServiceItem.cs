using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.IV
{
    public class IvServiceItem : EntityBase
    {
        public int ItemId { get; set; }
        public string OuCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemNameEnd { get; set; }
        public string IProductType { get; set; }
        public string Active { get; set; }
    }
}
