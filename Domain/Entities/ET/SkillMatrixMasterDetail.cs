using System;
using System.Collections.Generic;

namespace Domain.Entities.ET;

public class SkillMatrixMasterDetail : EntityBase
{
    public Guid Id { get; set; } = System.Guid.NewGuid();
    public Guid SkillMatrixMasterId { get; set; }
    public string Subject { get; set; }
    public Guid? SubjectGroup { get; set; }
    public string Descriptions { get; set; }
    public decimal MaxScore { get; set; }
    public ICollection<SkillMatrixMasterDetail> SkillMatrixMasterDetails { get; set; } = new List<SkillMatrixMasterDetail>();
}
