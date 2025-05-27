using Domain.Entities.TM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.Configurations.TM
{
    public class MemberHierarchyConfiguration : BaseConfiguration<MemberHierarchy>
    {
        public override void Configure(EntityTypeBuilder<MemberHierarchy> builder)
        {
            base.Configure(builder);
            builder.ToTable("member_hierarchy", "tm");
            builder.HasKey(e => e.UserId);
        }
    }
}
