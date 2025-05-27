using System;

namespace Domain.Entities.ET;

public class EvaluationDetail : EntityBase
{
    public Guid Id { get; set; } = System.Guid.NewGuid();
    public Guid EvaluationId { get; set; }
    public string Subject { get; set; }
    public Guid SubjectId { get; set; }
    public Guid? SubjectGroup { get; set; }
    public string Descriptions { get; set; }
    public decimal MaxScore { get; set; }
    public decimal? Score { get; set; } = 0;
}
