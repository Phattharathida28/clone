using System.Collections.Generic;

namespace Domain.Entities.SU;

public class Menu : EntityBase
{
    public string MenuCode { get; set; }
    public string SystemCode { get; set; }
    public string MainMenu { get; set; }
    public string ProgramCode { get; set; }
    public string Icon { get; set; }
    public bool Active { get; set; }
    public ICollection<Menu> SubMenus { get; set; }
    public ICollection<MenuLabel> MenuLabels { get; set; }
}