using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.IV;
using Domain.Entities.QO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.QO
{
    internal class QoSetupConfigurations : BaseConfiguration<QoSetup>
    {
        public override void Configure(EntityTypeBuilder<QoSetup> builder)
        {
            base.Configure(builder);
            builder.ToTable("qo_setup", "Back-End-Team");
            builder.HasKey(a => a.DataId);
            builder.HasAlternateKey(a => new { a.Active, a.OuCode, a.PlType });
        }
    }
}