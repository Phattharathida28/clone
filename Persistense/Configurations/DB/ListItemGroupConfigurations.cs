using Domain.Entities.DB;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Configurations.DB;

public class ListItemGroupConfigurations : BaseConfiguration<ListItemGroup>
{
    public override void Configure(EntityTypeBuilder<ListItemGroup> builder)
    {
        base.Configure(builder);
        builder.ToTable("list_item_group", "db");
        builder.HasKey(e => e.ListItemGroupCode);
        builder.HasMany(p => p.ListItems).WithOne().HasForeignKey(f => f.ListItemGroupCode).OnDelete(DeleteBehavior.Cascade);
    }
}
