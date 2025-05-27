using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.QO
{
    public class QoPriceMaster : EntityBase
    {
        public int PlId { get; set; }
        public string OuCode { get; set; }
        public string PlCode { get; set; }
        public string PlType { get; set; }
        public string PlNameTha { get; set; }
        public string PlNameEng { get; set; }
        public string CurrCode { get; set; }
        
        public string Remark { get; set; }
        public string VatType { get; set; }
        //public decimal ItemId { get; set; }
        //public decimal UnitId { get; set; }
        //public decimal? Price { get; set; }
        //public int? EditType { get; set; }
    }
}
