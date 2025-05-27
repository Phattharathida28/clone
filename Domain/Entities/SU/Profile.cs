using System.Collections.Generic;

namespace Domain.Entities.SU;

public class Profile : EntityBase
{
    public string ProfileCode { get; set; }
    public string Description { get; set; }
    public bool? Active { get; set; }
    public ICollection<ProfileLang> ProfileLangs { get; set; }
    public ICollection<ProfileMenu> ProfileMenus { get; set; }
}