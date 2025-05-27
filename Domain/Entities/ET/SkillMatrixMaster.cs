using System;
using System.Collections.Generic;

namespace Domain.Entities.ET;

public class SkillMatrixMaster : EntityBase
{
    public Guid Id { get; set; } = System.Guid.NewGuid();
    public string PositionCode { get; set; }
    public bool? Active { get; set; }
    public ICollection<SkillMatrixMasterDetail> SkillMatrixMasterDetails { get; set; } = new List<SkillMatrixMasterDetail>();
}
