using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IActivityLogService
{
    Task Log(ICleanDbContext context, string message, string activityTypeCode, CancellationToken token);
    Task LogForce(ICleanDbContext context, string message, string activityTypeCode, CancellationToken token);
}
