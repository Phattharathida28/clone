using Application.Interfaces;
using MediatR;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities.SU;
using System.Collections.Generic;

namespace Application.Features.SU.SURT01;

public class List
{
    public class Query : IRequest<List<Program>>
    {
        public string Keywords { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<Program>>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<List<Program>> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"select p.program_code ""programCode"", 
                                       p.program_name ""programName"", 
                                       p.program_path ""programPath"", 
                                       --case when @Lang = 'th' then li.list_item_name_tha else coalesce(li.list_item_name_eng, li.list_item_name_tha) end ""moduleCode"",
                                       '' ""moduleCode"",
                                       p.xmin ""rowVersion""
                                from su.program p 
                                --left join db.list_item li on li.list_item_group_code = 'Module' and p.module_code = li.list_item_code 
                                where 1=1");

            if (request.Keywords != null) sql.AppendLine(@"and lower(concat(p.program_code,p.program_name,p.program_path)) like lower(concat('%' , :Keywords , '%'))");

            sql.AppendLine("order by p.program_code");

            return await _context.QueryAsync<Program>(sql.ToString(), new { Lang = _user.Language, request.Keywords }, cancellationToken) as List<Program>;
        }
    }

}