using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Configurations.SU;

public class MenuConfuguration : BaseConfiguration<Menu>
{
    public override void Configure(EntityTypeBuilder<Menu> builder)
    {
        base.Configure(builder);
        builder.ToTable("menu", "su");
        builder.HasKey(e => e.MenuCode);
        builder.HasMany(p => p.SubMenus).WithOne().HasForeignKey(f => f.MainMenu).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p => p.MenuLabels).WithOne().HasForeignKey(f => f.MenuCode).OnDelete(DeleteBehavior.Cascade);
    }
}