using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.SU;

public class ContentConfiguration : BaseConfiguration<Content>
{
    public override void Configure(EntityTypeBuilder<Content> builder)
    {
        base.Configure(builder);
        builder.ToTable("content", "su");
        builder.HasKey(e => e.Id);
    }
}