using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.DB
{
    public class DbCurrencyConfigurations : BaseConfiguration<DbCurrency>
        {
            public override void Configure(EntityTypeBuilder<DbCurrency> builder)
            {
                base.Configure(builder);
                builder.ToTable("db_currency", "Back-End-Team");
                builder.HasKey(a => new { a.CurrCode, a.OuCode });
                
        }
        }
}
