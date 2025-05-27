using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.SURT03;

public class List
{
    public class Query : IRequest<List<Profile>>
    {
        public string Keywords { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<Profile>>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<List<Profile>> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"select 
	                                profile_code ""profileCode"",
	                                description ,
	                                active 
                                from su.profile p 
                                where concat(profile_code,description) ilike concat('%',@Keywords,'%')
                                order by profile_code");

            return await _context.QueryAsync<Profile>(sql.ToString(), new { request.Keywords }, cancellationToken) as List<Profile>;
        }
    }
}
