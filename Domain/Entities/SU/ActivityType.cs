using System.Collections.Generic;

namespace Domain.Entities.SU;

public class ActivityType : EntityBase
{
    public string ActivityTypeCode { get; set; }
    public string ActivityTypeName { get; set; }
    public string ActivityGroupCode { get; set; }
    public string LogTemplate { get; set; }
    public bool? Active { get; set; }
    public ICollection<ActivityLog> ActivityLogs { get; set; }
}
