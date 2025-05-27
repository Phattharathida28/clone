using System;

namespace Domain.Entities.SU;

public class ActivityLog : EntityBase
{
    public int Id { get; set; }
    public string ActivityTypeCode { get; set; }
    public string LogMessage { get; set; }
    public string LoggedBy { get; set; }
    public DateTime? LoggedDate { get; set; }
    public bool? Active { get; set; }

    public ActivityLog()
    {
        Active = true;
    }
}
