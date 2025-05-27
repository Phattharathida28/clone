using Application.Interfaces;
using Domain.Entities.ET;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ET.ETRT02;

public class List
{
    public class Query : IRequest<List<SkillMatrixMasterDTO>>
    {
        public string Keywords { get; set; }
    }

    public class SkillMatrixMasterDTO : SkillMatrixMaster
    {
        public string PositionName { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<SkillMatrixMasterDTO>>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<List<SkillMatrixMasterDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"select 
	                            smm.id,
	                            pl.position_name ""positionName"",
                                smm.active,
	                            smm.xmin ""rowVersion""
                            from et.skill_matrix_master smm
                            inner join db.position p on p.position_code = smm.position_code
                            inner join db.position_lang pl on p.position_code = pl.position_code and pl.language_code = upper(:lang)
                             ");

            if (request.Keywords != null) sql.AppendLine(@"where concat(pl.position_name) ilike lower(concat('%' , @Keywords , '%'))");

            sql.AppendLine("order by smm.position_code");

            return await _context.QueryAsync<SkillMatrixMasterDTO>(sql.ToString(), new { Lang = _user.Language, request.Keywords }, cancellationToken) as List<SkillMatrixMasterDTO>;
        }
    }
}
