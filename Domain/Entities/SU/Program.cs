using System.Collections.Generic;

namespace Domain.Entities.SU;

public class Program : EntityBase
{
    public string ProgramCode { get; set; }
    public string ProgramName { get; set; }
    public string ProgramPath { get; set; }
    public string SystemCode { get; set; }
    public string ModuleCode { get; set; }
    public ICollection<ProgramLabel> ProgramLabels { get; set; }
}