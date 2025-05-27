using Application.Interfaces;
using Domain.Entities.ET;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ET.ETRT01;

public class Detail
{
    public class Query : IRequest<EvaluationMaster>
    {
        public string Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, EvaluationMaster>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context)
        {
            _context = context;
        }
        public async Task<EvaluationMaster> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            EvaluationMaster evaluationMaster = new();

            sql.AppendLine(@"select 
                                em.id,
	                            em.position_code ""positionCode"",
	                            em.xmin ""rowVersion""
                            from et.evaluation_master em 
                            where em.id::text = :id");

            evaluationMaster = await _context.QueryFirstOrDefaultAsync<EvaluationMaster>(sql.ToString(), new { request.Id }, cancellationToken);

            if (evaluationMaster != null)
            {
                sql = new StringBuilder();
                sql.AppendLine(@"select 
	                                emd.id,
	                                emd.evaluation_master_id ""evaluationMasterId"",
	                                emd.subject,
	                                emd.subject_group ""subjectGroup"",
	                                emd.descriptions,
	                                emd.max_score ""maxScore"",
	                                emd.xmin ""rowVersion""
                                from et.evaluation_master_detail emd
                                where emd.evaluation_master_id::text = :id
                                order by emd.created_date");

                evaluationMaster.EvaluationMasterDetails = await _context.QueryAsync<EvaluationMasterDetail>(sql.ToString(), new { request.Id }, cancellationToken) as List<EvaluationMasterDetail>;

            }

            return evaluationMaster;
        }
    }
}
