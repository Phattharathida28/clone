using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.IN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;

namespace Persistense.Configurations.IN
{
    public class InGoodsConfigurations : BaseConfiguration<InGoods>
    {
        public override void Configure(EntityTypeBuilder<InGoods> builder)
        {
            base.Configure(builder);
            builder.ToTable("in_goods", "Back-End-Team");
            builder.HasKey(a => a.ItemId);
        }
    }
}
