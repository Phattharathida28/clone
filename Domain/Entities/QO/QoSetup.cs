using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.QO
{
    public class QoSetup : EntityBase

    {
        public int DataId { get; set; }
        public string OuCode { get; set; }
        public string PlType { get; set; }
        public bool Active { get; set; }
        public string QoSetupNameTha { get; set; }
        public string QoSetupNameEng { get; set; }
        public string RunningNo { get; set; }
        public decimal? Digit { get; set; }

    }
}
