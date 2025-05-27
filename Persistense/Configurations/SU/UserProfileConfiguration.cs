using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.SU;

public class UserProfileConfiguration : BaseConfiguration<UserProfile>
{
    public override void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        base.Configure(builder);
        builder.ToTable("user_profile", "su");
        builder.HasKey(e => new { e.UserId, e.ProfileCode });
    }
}