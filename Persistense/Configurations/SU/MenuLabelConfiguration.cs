using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.SU;

public class MenuLabelConfiguration : BaseConfiguration<MenuLabel>
{
    public override void Configure(EntityTypeBuilder<MenuLabel> builder)
    {
        base.Configure(builder);
        builder.ToTable("menu_label", "su");
        builder.HasKey(e => new { e.MenuCode, e.LanguageCode });
    }
}