using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.SURT03;

public class Master
{
    public class MasterData
    {
        public IEnumerable<dynamic> Menus { get; set; }
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
            master.Menus = await GetMenu(cancellationToken);

            return master;
        }
        private async Task<IEnumerable<dynamic>> GetMenu(CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"select sm.menu_code as ""menuCode"", sml.menu_name as ""menuName"", icon
                            from su.menu sm
                            inner join su.menu_label sml on sm.menu_code = sml.menu_code and lower(sml.language_code) = lower(@lang)
                            order by sm.menu_code"
            );

            return await _context.QueryAsync<dynamic>(sql.ToString(), new { lang = _user.Language }, cancellationToken);
        }
    }
}
