using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.DB.DBRT02;

public class Master
{
    public class MasterList
    {
        public IEnumerable<dynamic> Langs { get; set; }
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

            master.Langs = await _context.QueryAsync<dynamic>(@"
            select 
                l.language_code ""value"", case when ll.language_name is null then l.language_code else ll.language_name end ""label"", l.pattern
            from db.""language"" l 
            left join db.language_lang ll on l.language_code = ll.language_code and ll.language_code_forname = upper(:Lang)
            where l.active = true
            order by l.language_code, ll.language_name", new { Lang = _user.Language }, cancellationToken);

            return master;
        }
    }
}
