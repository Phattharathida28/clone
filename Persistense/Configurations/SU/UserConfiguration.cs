using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;


namespace Persistense.Configurations.SU;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public virtual void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);
        builder.ToTable("user", "su");
        builder.Property(p => p.Id).HasColumnName("user_id");
        builder.Ignore(p => p.Password);
        builder.Ignore(p => p.TwoFactorEnabled);
        builder.Ignore(p => p.PhoneNumberConfirmed);
        builder.Ignore(p => p.NormalizedEmail);
        builder.Ignore(p => p.EmailConfirmed);
        builder.Ignore(p => p.RowVersion);
        builder.HasMany(o => o.UserProfiles).WithOne().HasForeignKey(p => p.UserId);
        builder.Property(e => e.CreatedBy).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        builder.Property(e => e.CreatedDate).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        builder.Property(e => e.CreatedProgram).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
    }
}