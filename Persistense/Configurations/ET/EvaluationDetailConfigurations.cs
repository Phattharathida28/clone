using Domain.Entities.ET;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.ET;

public class EvaluationDetailConfigurations : BaseConfiguration<EvaluationDetail>
{
    public override void Configure(EntityTypeBuilder<EvaluationDetail> builder)
    {
        base.Configure(builder);
        builder.ToTable("evaluation_detail", "et");
        builder.HasKey(e => e.Id);
    }
}