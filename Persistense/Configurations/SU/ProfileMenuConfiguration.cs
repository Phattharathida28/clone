using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.SU;

public class ProfileMenuConfiguration : BaseConfiguration<ProfileMenu>
{
    public override void Configure(EntityTypeBuilder<ProfileMenu> builder)
    {
        base.Configure(builder);
        builder.ToTable("profile_menu", "su");
        builder.HasKey(e => new { e.ProfileCode, e.MenuCode });
        builder.Property(e => e.ProfileCode).ValueGeneratedOnAdd();
    }
}
