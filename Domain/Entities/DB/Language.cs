using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.DB;

public class Language : EntityBase
{
    [MaxLength(20)]
    public string LanguageCode { get; set; }
    public string Description { get; set; }
    [MaxLength(200)]
    public string Pattern { get; set; }
    public bool Active { get; set; } = true;
    public ICollection<LanguageLang> LanguageLangs { get; set; } = new List<LanguageLang>();
}