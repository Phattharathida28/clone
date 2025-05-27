using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.ET.ETRT02;

public class Master
{
    public class MasterList
    {
        public IEnumerable<dynamic> Positions { get; set; }
    }
    public class Query : IRequest<MasterList>
    {
        public class Handler : IRequestHandler<Query, MasterList>
        {
            private readonly ICleanDbContext _context;
            private readonly ICurrentUserAccessor _user;

            public Handler(ICleanDbContext context, ICurrentUserAccessor user)
            {
                _context = context;
                _user = user;
            }
            public async Task<MasterList> Handle(Query request, CancellationToken cancellationToken)
            {
                MasterList master = new MasterList();

                master.Positions = await _context.QueryAsync<dynamic>(@"
                select 
	                p.position_code ""value"", pl.position_name ""label""
                from db.""position"" p 
                inner join db.position_lang pl on p.position_code = pl.position_code and pl.language_code = upper(:lang)
                where p.active = true", new { lang = _user.Language }, cancellationToken);

                return master;
            }
        }
    }
}
