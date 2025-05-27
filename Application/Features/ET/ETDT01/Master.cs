using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.ET.ETDT01;

public class Master
{
    public class MasterList
    {
        public IEnumerable<dynamic> Statuses { get; set; }
    }

    public class Query : IRequest<MasterList>
    {

    }

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

            master.Statuses = await _context.QueryAsync<dynamic>(@"
            select 
	            s.id ""value"", sl.""name"" ""label"", s.seq
            from db.status s 
            inner join db.status_lang sl on s.id = sl.status_id and sl.language_code = upper(:lang)
            order by sl.""name""", new { Lang = _user.Language }, cancellationToken);

            return master;
        }
    }
}
