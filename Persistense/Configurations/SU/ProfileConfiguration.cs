using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.SU;

namespace Persistense.Configurations.SU;

public class ProfileConfiguration : BaseConfiguration<Profile>
{
    public override void Configure(EntityTypeBuilder<Profile> builder)
    {
        base.Configure(builder);
        builder.ToTable("profile", "su");
        builder.HasKey(e => e.ProfileCode);
        builder.HasMany(e => e.ProfileLangs).WithOne().HasForeignKey(o => o.ProfileCode).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(e => e.ProfileMenus).WithOne().HasForeignKey(o => o.ProfileCode);
    }
}