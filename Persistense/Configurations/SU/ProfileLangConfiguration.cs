using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.SU;

public class ProfileLangConfiguration : BaseConfiguration<ProfileLang>
{
    public override void Configure(EntityTypeBuilder<ProfileLang> builder)
    {
        base.Configure(builder);
        builder.HasKey(e => new { e.ProfileCode, e.LanguageCode });
        builder.Property(e => e.ProfileCode).ValueGeneratedOnAdd();
    }
}
