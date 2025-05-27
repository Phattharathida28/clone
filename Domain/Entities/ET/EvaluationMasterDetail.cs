using System;
using System.Collections.Generic;

namespace Domain.Entities.ET;

public class EvaluationMasterDetail : EntityBase
{
    public Guid Id { get; set; } = System.Guid.NewGuid();
    public Guid EvaluationMasterId { get; set; }
    public string Subject { get; set; }
    public Guid? SubjectGroup { get; set; }
    public string Descriptions { get; set; }
    public decimal MaxScore { get; set; }
    public ICollection<EvaluationMasterDetail> EvaluationMasterDetails { get; set; } = new List<EvaluationMasterDetail>();
}
