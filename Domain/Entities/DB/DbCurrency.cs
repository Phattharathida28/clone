using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DB
{
    public class DbCurrency : EntityBase
    {
        public string CurrCode { get; set; }
        public string? OuCode { get; set; }
        public string? CurNameTha { get; set; }
        public string? CurNameEng { get;set; }
        public string Active { get; set; }


    }
}
