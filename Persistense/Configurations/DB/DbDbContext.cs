using Application.Interfaces;
using Domain.Entities.DB;
using Microsoft.EntityFrameworkCore;


namespace Persistense;

public partial class CleanDbContext : DbContext, ICleanDbContext
{
    public DbSet<Language> languages {  get; set; }
    public DbSet<LanguageLang> languageLangs {  get; set; }
    public DbSet<ListItemGroup> listItemGroups {  get; set; }
    public DbSet<ListItem> listItemGroupLangs {  get; set; }

    public DbSet<DbCurrency>  dbCurrency { get; set; }
    public DbSet<DbUnit> dbUnit { get; set; }
}
