using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.DB;
using Domain.Entities.IV;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.IV
{
    public class IvServiceItemConfigurations : BaseConfiguration<IvServiceItem>
    {
        public override void Configure(EntityTypeBuilder<IvServiceItem> builder)
        {
            base.Configure(builder);
            builder.ToTable("iv_service_item", "Back-End-Team");
            builder.HasKey(a => a.ItemId);
            builder.HasAlternateKey(a => new { a.ItemCode, a.OuCode});
        }
    }
}
