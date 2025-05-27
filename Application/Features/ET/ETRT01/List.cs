using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities.ET;

namespace Application.Features.ET.ETRT01;

public class List
{
    public class Query : IRequest<List<EvaluationDTO>>
    {
        public string Keywords { get; set; }
    }

    public class EvaluationDTO : EvaluationMaster
    {
        public string PositionName { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Query, List<EvaluationDTO>>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<List<EvaluationDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"select
	                                em.id,
	                                --get_wording(@lang, p.position_name_th, p.position_name_en) ""positionName"",
                                    em.position_code ""positionName"",
	                                em.active,
	                                em.xmin ""rowVersion""
                                from et.evaluation_master em 
                                inner join db.""position"" p on p.position_code = em.position_code");

            if (request.Keywords != null) sql.AppendLine(@"where 
                                                            --get_wording(@lang, p.position_name_th, p.position_name_en) like lower(concat('%' , @Keywords , '%')
                                                            lower(em.position_code) like lower(concat('%' , @Keywords , '%')
                                                        )");

            sql.AppendLine("order by \"positionName\"");

            return await _context.QueryAsync<EvaluationDTO>(sql.ToString(), new { Lang = _user.Language, request.Keywords }, cancellationToken) as List<EvaluationDTO>;
        }
    }
}
