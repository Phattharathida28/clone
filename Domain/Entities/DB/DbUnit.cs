using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DB
{
    public class DbUnit : EntityBase
    {
        public int UnitId {  get; set; }
        public string OuCode { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string UnitNameEng { get; set; }
        public string Active {  get; set; }
        
    }
}
