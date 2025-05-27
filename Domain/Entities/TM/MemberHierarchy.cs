using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.TM
{
    public class MemberHierarchy : EntityBase
    {
        public int UserId { get; set; }
        public int? HeadUser { get; set; }
    }
}
