using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.DB;

namespace Persistense.Configurations.DB;

public class LanguageConfigurations : BaseConfiguration<Language>
{
    public override void Configure(EntityTypeBuilder<Language> builder)
    {
        base.Configure(builder);
        builder.ToTable("language", "db");
        builder.HasKey(e => e.LanguageCode);
        builder.HasMany(p => p.LanguageLangs).WithOne().HasForeignKey(f => f.LanguageCode).OnDelete(DeleteBehavior.Cascade);
    }
}