using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.TM
{
    public class Employee : EntityBase
    {
        public string EmployeeCode { get; set; }
        public string EmployeeFirstnameTh { get; set; }
        public string EmployeeSurnameTh { get; set;}
        public string EmployeeNameEn { get; set; }
        public string EmployeeSurnameEn { get; set; }
        public Guid TeamCode { get; set; }
        public bool Gender { get; set; }
        public DateTime BateOfBirth { get; set; }
        public string Nationality { get; set;}
        public string Religion { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set;}
        public string PositionCode { get; set;}
        public int UserId { get; set;}
        public Guid SequeneId { get; set;}
        public string LanguageCode { get; set;}
        public bool Active { get; set;}
    }
}
