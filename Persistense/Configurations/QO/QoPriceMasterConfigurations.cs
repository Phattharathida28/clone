using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.QO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.QO
{
    internal class QoPriceMasterConfigurations : BaseConfiguration<QoPriceMaster>
    {
        public override void Configure(EntityTypeBuilder<QoPriceMaster> builder)
        {
            base.Configure(builder);
            builder.ToTable("qo_price_master", "Back-End-Team");
            builder.HasKey(a => a.PlId);
            builder.HasAlternateKey(a => new { a.OuCode, a.PlCode });
        }
    }
}
