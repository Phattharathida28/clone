using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.DB;

public class LanguageLang: EntityBase
{
    [MaxLength(20)]
    public string LanguageCode { get; set; }
    [MaxLength(20)]
    public string LanguageCodeForname { get; set; }
    [MaxLength(200)]
    public string LanguageName { get; set; }
}
