using Application.Interfaces;
using Domain.Entities.DB;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.DB.DBRT01;

public class List
{
    public class Query : IRequest<List<Language>>
    {
        public string Keywords { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<Language>>
    {
        private readonly ICleanDbContext _context;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user) => _context = context;

        public async Task<List<Language>> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"
            select 
                l.language_code ""languageCode"",
                l.description,
                l.active
            from db.""language"" l 
            where concat(l.language_code, l.description) ilike concat('%', coalesce(:Keywords, ''), '%')
            order by ""languageCode"", l.description");

            return await _context.QueryAsync<Language>(sql.ToString(), new { request.Keywords }, cancellationToken) as List<Language>;
        }
    }
}
