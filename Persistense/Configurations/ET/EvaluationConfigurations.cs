using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.ET;

namespace Persistense.Configurations.ET;

public class EvaluationConfigurations : BaseConfiguration<Evaluation>
{
    public override void Configure(EntityTypeBuilder<Evaluation> builder)
    {
        base.Configure(builder);
        builder.ToTable("evaluation", "et");
        builder.HasKey(e => e.Id);
        builder.HasMany(p => p.EvaluationDetails).WithOne().HasForeignKey(f => f.EvaluationId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p => p.SkillMatrices).WithOne().HasForeignKey(f => f.EvaluationId).OnDelete(DeleteBehavior.Cascade);
    }
}
