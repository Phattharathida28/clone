using Application.Interfaces;
using Domain.Entities.ET;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ET.ETRT02;

public class Detail
{
    public class Query : IRequest<SkillMatrixMaster>
    {
        public string Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, SkillMatrixMaster>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context)
        {
            _context = context;
        }
        public async Task<SkillMatrixMaster> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            SkillMatrixMaster skillMatrixMaster = new();

            sql.AppendLine(@"select 
	                            smm.id,
	                            smm.position_code ""positionCode"",
	                            smm.active,
	                            smm.xmin ""rowVersion""
                            from et.skill_matrix_master smm
                            where smm.id::text = :id");

            skillMatrixMaster = await _context.QueryFirstOrDefaultAsync<SkillMatrixMaster>(sql.ToString(), new { request.Id }, cancellationToken);

            if (skillMatrixMaster != null)
            {
                sql = new StringBuilder();
                sql.AppendLine(@"select 
	                                smmd.id,
	                                smmd.skill_matrix_master_id ""skillMatrixMasterId"",
	                                smmd.subject,
	                                smmd.subject_group ""subjectGroup"",
	                                smmd.descriptions,
	                                smmd.max_score ""maxScore"",
	                                smmd.xmin ""rowVersion""
                                from et.skill_matrix_master_detail smmd 
                                where smmd.skill_matrix_master_id::text = :id");

                skillMatrixMaster.SkillMatrixMasterDetails = await _context.QueryAsync<SkillMatrixMasterDetail>(sql.ToString(), new { request.Id }, cancellationToken) as List<SkillMatrixMasterDetail>;
            }

            return skillMatrixMaster;
        }
    }
}
