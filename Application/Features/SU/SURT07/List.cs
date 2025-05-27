using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities.SU;

namespace Application.Features.SU.SURT07;

public class List
{
    public class Query : IRequest<List<Content>>
    {
        public string Keywords { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<Content>>
    {
        private readonly ICleanDbContext _context;

        public Handler(ICleanDbContext context) => _context = context;

        public async Task<List<Content>> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"select 
	                            c.id ,
	                            c.""name"" ,
	                            c.""path"" ,
	                            c.""size"" ,
	                            c.created_by ""createdBy"",
	                            c.created_date ""createdDate"",
                                c.container
                            from su.""content"" c 
                            where concat(c.""name"", c.""path"", c.""size"", c.created_by) ilike concat('%', @Keywords, '%')
                            order by c.created_date desc
                ");

            return await _context.QueryAsync<Content>(sql.ToString(), new { request.Keywords }, cancellationToken) as List<Content>;
        }
    }
}