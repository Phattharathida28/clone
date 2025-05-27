using Application.Interfaces;
using MediatR;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Application.Features.SU.SURT04;

public class List
{
    public class Query : IRequest<List<Response>>
    {
        public string Keywords { get; set; }
    }

    public class Response
    {
        public long UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string FirstnameTh { get; set; }
        public string LastnameTh { get; set; }
        public bool Active { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<Response>>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<List<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"SELECT 
	                                user_id ""userId"", 
	                                firstname, 
	                                lastname,
                                    firstname_th ""firstnameTh"",
                                    lastname_th ""lastnameTh"",
	                                active
                                FROM su.user
                                where lower(concat(user_id,firstname,lastname, firstname_th, lastname_th)) ilike lower(concat('%',@Keywords,'%'))
                                order by firstname, lastname");

            return await _context.QueryAsync<Response>(sql.ToString(), new { request.Keywords }, cancellationToken) as List<Response>;
        }
    }
}