using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.DB;
using Domain.Entities.IN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistense.Configurations;

namespace Persistense.Configurations.DB
{
    public class DbUnitConfigurations : BaseConfiguration<DbUnit>
    {
        public override void Configure(EntityTypeBuilder<DbUnit> builder)
        {
            base.Configure(builder);
            builder.ToTable("db_unit", "Back-End-Team");
            builder.HasKey(a => a.UnitId);
            builder.HasAlternateKey(a => new { a.OuCode, a.UnitCode});
        }
    }
}


    