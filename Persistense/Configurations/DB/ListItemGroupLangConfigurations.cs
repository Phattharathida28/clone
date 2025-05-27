using Domain.Entities.DB;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Configurations.DB;

public class ListItemGroupLangConfigurations : BaseConfiguration<ListItem>
{
    public override void Configure(EntityTypeBuilder<ListItem> builder)
    {
        base.Configure(builder);
        builder.ToTable("list_item_group_lang", "db");
        builder.HasKey(e => new { e.ListItemGroupCode });
    }
}