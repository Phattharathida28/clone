using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.SU;

public class ApiClientConfiguration : BaseConfiguration<ApiClient>
{
    public override void Configure(EntityTypeBuilder<ApiClient> builder)
    {
        base.Configure(builder);
        builder.HasKey(e => e.Id);
    }
}