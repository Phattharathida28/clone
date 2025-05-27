using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities.DB;
using Domain.Entities.IN;
using Microsoft.EntityFrameworkCore;

namespace Persistense;

public partial class CleanDbContext : DbContext, ICleanDbContext
{
    public DbSet<InGoods> inGoods { get; set; }
}


