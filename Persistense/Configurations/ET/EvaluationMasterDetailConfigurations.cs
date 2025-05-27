using Domain.Entities.ET;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Configurations.ET;

public class EvaluationMasterDetailConfigurations : BaseConfiguration<EvaluationMasterDetail>
{
    public override void Configure(EntityTypeBuilder<EvaluationMasterDetail> builder)
    {
        base.Configure(builder);
        builder.ToTable("evaluation_master_detail", "et");
        builder.HasKey(e => e.Id);
        builder.HasMany(e => e.EvaluationMasterDetails).WithOne().HasForeignKey(e => e.SubjectGroup).OnDelete(DeleteBehavior.Cascade);
    }
}