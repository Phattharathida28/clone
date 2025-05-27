using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Configurations.SU;

public class ProgramConfiguration : BaseConfiguration<Program>
{
    public override void Configure(EntityTypeBuilder<Program> builder)
    {
        base.Configure(builder);
        builder.ToTable("program", "su");
        builder.HasKey(e => e.ProgramCode);
        builder.HasMany(e => e.ProgramLabels).WithOne().HasForeignKey(o => o.ProgramCode).OnDelete(DeleteBehavior.Cascade);
    }
}
