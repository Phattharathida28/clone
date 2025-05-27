using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.SU.SURT01;

public class Master
{
    public class MasterList
    {
        public IEnumerable<dynamic> Lang { get; set; }
        public IEnumerable<dynamic> Modules { get; set; }
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

                master.Lang = await _context.QueryAsync<dynamic>(@"select l.language_code as value , 
	                                                                           ll.language_name as label 
                                                                       from db.language l 
                                                                       inner join db.language_lang ll on l.language_code = ll.language_code 
                                                                       where lower(ll.language_code_forname) = lower(@language)
                                                                       order by case when l.language_code = 'TH' then 1 end", new { language = _user.Language }, cancellationToken);

                //master.Modules = await _context.QueryAsync<dynamic>(@"select dli.list_item_code as value , 
                //                                                                 dli.list_item_code || ' : ' || case when @language = 'th' then dli.list_item_name_tha else coalesce(dli.list_item_name_eng, dli.list_item_name_tha) end as label 
                //                                                          from db.list_item dli 
                //                                                          where dli.list_item_group_code = 'Module' 
                //                                                          order by dli.sequence", new { language = _user.Language }, cancellationToken);
                
                master.Modules = await _context.QueryAsync<dynamic>(@"select 'DB' ""value"", 'DB : Master' ""label""", new {  }, cancellationToken);

                return master;
            }
        }
    }
}