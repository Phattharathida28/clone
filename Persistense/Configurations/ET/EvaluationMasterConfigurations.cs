using Domain.Entities.ET;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Configurations.ET;

public class EvaluationMasterConfigurations : BaseConfiguration<EvaluationMaster>
{
    public override void Configure(EntityTypeBuilder<EvaluationMaster> builder)
    {
        base.Configure(builder);
        builder.ToTable("evaluation_master", "et");
        builder.HasKey(e => e.Id);
        builder.HasMany(p => p.EvaluationMasterDetails).WithOne().HasForeignKey(f => f.EvaluationMasterId).OnDelete(DeleteBehavior.Cascade);
    }
}