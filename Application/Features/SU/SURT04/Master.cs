using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.SU.SURT04;

public class Master
{
    public class MasterData
    {
        public IEnumerable<dynamic> Profiles { get; set; }
    }

    public class Query : IRequest<MasterData>
    {

    }

    public class Handler : IRequestHandler<Query, MasterData>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<MasterData> Handle(Query request, CancellationToken cancellationToken)
        {
            MasterData master = new MasterData();
            master.Profiles = await GetProfiles(cancellationToken);

            return master;
        }
        private async Task<IEnumerable<dynamic>> GetProfiles(CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"select profile_code as value, profile_code || ' : ' || description as label from su.profile where active = true");

            return await _context.QueryAsync<dynamic>(sql.ToString(), new { lang = _user.Language }, cancellationToken);
        }
    }
}