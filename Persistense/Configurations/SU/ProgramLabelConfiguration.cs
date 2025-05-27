using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistense.Configurations.SU;

public class SuProgramLabelConfiguration : BaseConfiguration<ProgramLabel>
{
    public override void Configure(EntityTypeBuilder<ProgramLabel> builder)
    {
        base.Configure(builder);
        builder.ToTable("program_label", "su");
        builder.HasKey(e => new { e.ProgramCode, e.FieldName, e.LanguageCode });
        builder.Property(e => e.ProgramCode).ValueGeneratedOnAdd();
    }
}
