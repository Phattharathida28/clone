using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Configurations.SU;
public class MessageConfiguration : BaseConfiguration<Message>
{
    public override void Configure(EntityTypeBuilder<Message> builder)
    {
        base.Configure(builder);
        builder.ToTable("message", "su");
        builder.HasKey(e => new {e.MessageCode , e.LanguageCode});
    }
}
