using Domain.Entities.ET;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Configurations.ET;

public class SkillMatrixMasterDetailConfigurations : BaseConfiguration<SkillMatrixMasterDetail>
{
    public override void Configure(EntityTypeBuilder<SkillMatrixMasterDetail> builder)
    {
        base.Configure(builder);
        builder.ToTable("skill_matrix_master_detail", "et");
        builder.HasKey(e => e.Id);
        builder.HasMany(e => e.SkillMatrixMasterDetails).WithOne().HasForeignKey(e => e.SubjectGroup).OnDelete(DeleteBehavior.Cascade);
    }
}