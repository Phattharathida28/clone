using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Configurations.SU;

public class ActivityTypeConfiguration : BaseConfiguration<ActivityType>
{
    public override void Configure(EntityTypeBuilder<ActivityType> builder)
    {
        base.Configure(builder);
        builder.ToTable("activity_type", "su");
        builder.HasKey(e => e.ActivityTypeCode);
        builder.HasMany(e => e.ActivityLogs).WithOne().HasForeignKey(o => o.ActivityTypeCode).OnDelete(DeleteBehavior.Cascade);
    }
}