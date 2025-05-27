using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DB
{
    public class ListItem : EntityBase
    {
        public string ListItemGroupCode { get; set; }
        public string ListItemCode { get; set; }
        public string ListItemNameTha { get; set; }
        public string ListItemNameEng { get; set; }
        public int? Sequence { get; set; }
        public string Remark { get; set; }
        public string Attibute1 { get; set; }
        public string Attibute2 { get; set; }
        public string Attibute3 { get; set; }
        public string Attibute4 { get; set; }
        public string Attibute5 { get; set; }
        public string ListItemShortNameTha { get; set; }
        public string ListItemShortNameEng { get; set; }
        public string ListItemCodeMua { get; set; }
        public string ListItemCodeTha { get; set; }
        public string ListItemCodeEng { get; set; }
        public bool Active { get; set; }
    }
}
