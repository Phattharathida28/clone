using Application.Interfaces;
using Domain.Entities.TM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense
{
    public partial class CleanDbContext : DbContext, ICleanDbContext
    {
        DbSet<Employee> employees { get; set; }
        DbSet<MemberHierarchy> memberHierarchies { get; set; }
    }
}
