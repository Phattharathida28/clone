using Domain.Entities.DB;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Configurations.DB;

public class LanguageLangConfigurations : BaseConfiguration<LanguageLang>
{
    public override void Configure(EntityTypeBuilder<LanguageLang> builder)
    {
        base.Configure(builder);
        builder.ToTable("language_lang", "db");
        builder.HasKey(e => new { e.LanguageCode, e.LanguageCodeForname});
    }
}