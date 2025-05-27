using System;
using System.Collections.Generic;

namespace Domain.Entities.ET;

public class Evaluation : EntityBase
{
    public Guid Id { get; set; }
    public string EmployeeCode { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid Status { get; set; }
    public string SoftSkills { get; set; }
    public string HardSkills { get; set; }
    public string Goals { get; set; }
    public ICollection<EvaluationDetail> EvaluationDetails { get; set; } = new List<EvaluationDetail>();
    public ICollection<SkillMatrix> SkillMatrices { get; set; } = new List<SkillMatrix>();
}
