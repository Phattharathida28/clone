using Domain.Entities.ET;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.ET;

public class SkillMatrixMasterConfigurations : BaseConfiguration<SkillMatrixMaster>
{
    public override void Configure(EntityTypeBuilder<SkillMatrixMaster> builder)
    {
        base.Configure(builder);
        builder.ToTable("skill_matrix_master", "et");
        builder.HasKey(e => e.Id);
        builder.HasMany(p => p.SkillMatrixMasterDetails).WithOne().HasForeignKey(f => f.SkillMatrixMasterId).OnDelete(DeleteBehavior.Cascade);
    }
}