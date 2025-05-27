using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Entities.ET;

public class EvaluationMaster : EntityBase
{
    public Guid Id { get; set; } = System.Guid.NewGuid();
    public string PositionCode { get; set; }
    public bool Active { get; set; } = true;
    public ICollection<EvaluationMasterDetail> EvaluationMasterDetails { get; set; } = new List<EvaluationMasterDetail>();
}
