using Application.Interfaces;
using Domain.Entities.SU;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class ActivityLogService : IActivityLogService
{
    private readonly ICurrentUserAccessor _currentUserAccessor;
    public ActivityLogService(ICurrentUserAccessor currentUserAccessor)
    {
        _currentUserAccessor = currentUserAccessor;
    }

    public async Task Log(ICleanDbContext context, string message, string activityTypeCode, CancellationToken token)
    {
        ActivityLog activityLog = new();
        activityLog.ActivityTypeCode = activityTypeCode;
        activityLog.Active = true;
        activityLog.LogMessage = message;
        activityLog.LoggedBy = _currentUserAccessor.UserName;
        activityLog.LoggedDate = DateTime.Now;

        context.Set<ActivityLog>().Add(activityLog);
        await context.SaveChangesAsync(token);
    }

    public async Task LogForce(ICleanDbContext context, string message, string activityTypeCode, CancellationToken token)
    {
        await context.ExecuteAsync(@"
        INSERT INTO su.activity_log
        (activity_type_code, 
        log_message, 
        logged_by, 
        logged_date, 
        active, 
        created_by, 
        created_date, 
        created_program, 
        updated_by, 
        updated_date, 
        updated_program)
        VALUES(
        @ActivityTypeCode, 
        @Message, 
        @User, 
        now(), 
        true, 
        @User, 
        now(), 
        @Program, 
        @User, 
        now(), 
        @Program
        );    
        ",
        new
        {
            ActivityTypeCode = activityTypeCode,
            Message = message,
            User = _currentUserAccessor.UserName,
            Program = _currentUserAccessor.ProgramCode
        },
        token);
    }
}
