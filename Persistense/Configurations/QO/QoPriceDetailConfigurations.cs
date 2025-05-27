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
    public class InGoodConfigurations : BaseConfiguration<QoPriceDetail>
    {
        public override void Configure(EntityTypeBuilder<QoPriceDetail> builder)
        {
            base.Configure(builder);
            builder.ToTable("qo_price_detail", "Back-End-Team");

            builder.HasKey(a => a.PlDetId);
            builder.HasAlternateKey(a => new { a.OuCode, a.PlId, a.ItemId, a.UnitId });

        }
    }
}
