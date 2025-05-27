using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Configurations.SU;

public class ActivityLogConfiguration : BaseConfiguration<ActivityLog>
{
    public override void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        base.Configure(builder);
        builder.ToTable("activity_log", "su");
        builder.HasKey(e => e.Id);
    }
}