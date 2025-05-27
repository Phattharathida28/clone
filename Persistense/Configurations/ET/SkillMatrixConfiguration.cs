using Domain.Entities.ET;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.ET;

public class SkillMatrixConfiguration : BaseConfiguration<SkillMatrix>
{
    public override void Configure(EntityTypeBuilder<SkillMatrix> builder)
    {
        base.Configure(builder);
        builder.ToTable("skill_matrix", "et");
        builder.HasKey(e => e.Id);
    }
}