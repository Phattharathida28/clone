using Application.Common.Models;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Interfaces;

public interface ICleanDbContext : IDbContext
{
    Task<PageDto> GetPage(string sql, object param, RequestPageQuery page, CancellationToken token = default);
}
