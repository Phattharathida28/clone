using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;

namespace Domain.Entities.SU;

public class User : IdentityUser<long>
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Password { get; set; }
    public string PasswordPolicyCode { get; set; }
    public string DefaultLang { get; set; }
    public bool Active { get; set; }
    public bool ForceChangePassword { get; set; }
    public DateTime? StartEffectiveDate { get; set; }
    public DateTime? EndEffectiveDate { get; set; }
    public DateTime? LastChangePassword { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string CreatedProgram { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedProgram { get; set; }
    public byte[] RowVersion { get; set; }
    public bool? Ripple { get; set; }
    public string Inputstyle { get; set; }
    public string Menumode { get; set; }
    public string Colorscheme { get; set; }
    public string Color { get; set; }
    public int? Scale { get; set; }
    public string FirstnameTh { get; set; }
    public string LastnameTh { get; set; }
    public ICollection<UserProfile> UserProfiles { get; set; }

    public User()
    {
        PasswordPolicyCode = "001";
        DefaultLang = "TH";
        Ripple = true;
        Inputstyle = "outlined";
        Menumode = "static";
        Colorscheme = "system";
        Color = "#3B82F6";
        Scale = 14;
    }
}