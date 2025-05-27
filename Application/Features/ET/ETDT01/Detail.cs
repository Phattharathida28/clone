using Application.Interfaces;
using Domain.Entities.ET;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.ET.ETDT01;

public class Detail
{
    public class Query : IRequest<EvaluationDTO>
    {
        public string Id { get; set; }
    }

    public class EvaluationDTO : Evaluation
    {
        public string UserName { get; set; }
    }

    public class Handler : IRequestHandler<Query, EvaluationDTO>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<EvaluationDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            string sql = string.Empty;
            EvaluationDTO evaluation = new();

            sql = @"
            select 
	            e.id,
	            e.employee_code ""employeeCode"",
	            e.start_date ""startDate"",
	            e.end_date ""endDate"",
                e.soft_skills ""softSkills"",
                e.hard_skills ""hardSkills"",
                e.goals,
	            e.status,
                u.user_name ""userName"",
                e.xmin ""rowVersion""
            from et.evaluation e
            inner join db.employee e2 on e2.employee_code = e.employee_code
            inner join su.user u on e2.user_id = u.user_id
            where e.id::text = :Id";

            evaluation = await _context.QueryFirstOrDefaultAsync<EvaluationDTO>(sql, new { request.Id }, cancellationToken) ?? new();

            if (evaluation.UserName == null)
            {
                evaluation.UserName = _user.UserName;
            }

            sql = string.Empty;

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                sql = @$"
                select
                    emd.id ""subjectId"",
                    null ""evaluationId"",
                    emd.subject,
                    emd.subject_group ""subjectGroup"",
                    emd.descriptions,
                    emd.max_score ""maxScore"",
                    0 ""score""
                from et.evaluation_master em
                inner join et.evaluation_master_detail emd on em.id = emd.evaluation_master_id
                inner join db.employee e on e.position_code = em.position_code 
                inner join su.""user"" u on u.user_id = e.user_id
                where u.user_name = :User
                order by emd.created_date";
            }
            else
            {
                sql = @"
                select 
	                ed.id,
                    ed.subject_id ""subjectId"",
	                ed.evaluation_id ""evaluationId"",
	                ed.subject,
	                ed.subject_group ""subjectGroup"",
	                ed.descriptions,
	                ed.max_score ""maxScore"",
	                ed.score ""score"",
	                ed.xmin ""rowVersion""
                from et.evaluation e 
                inner join et.evaluation_detail ed on e.id = ed.evaluation_id
                where e.id::text = :Id
                order by ed.created_date";
            }

            evaluation.EvaluationDetails = await _context.QueryAsync<EvaluationDetail>(sql.ToString(), new { request.Id, User = _user.UserName }, cancellationToken) as List<EvaluationDetail>;

            sql = string.Empty;

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                sql = @$"
                select
                    smmd.id ""subjectId"",
                    null ""evaluationId"",
                    smmd.subject,
                    smmd.subject_group ""subjectGroup"",
                    smmd.descriptions,
                    smmd.max_score ""maxScore"",
                    0 ""score"",
                    smmd.xmin ""rowVersion""
                from et.skill_matrix_master smm
                inner join et.skill_matrix_master_detail smmd on smm.id = smmd.skill_matrix_master_id 
                inner join db.employee e on e.position_code = smm.position_code 
                inner join su.""user"" u on u.user_id = e.user_id
                where u.user_name = :User
                order by smm.created_date";
            }
            else
            {
                sql = @"
                select 
                    sm.id,
                    sm.subject_id ""subjectId"",
                    sm.evaluation_id ""evaluationId"",
                    sm.subject,
                    sm.subject_group ""subjectGroup"",
                    sm.descriptions,
                    sm.max_score ""maxScore"",
                    sm.score ""score"",
                    sm.xmin ""rowVersion""
                from et.evaluation e 
                inner join et.skill_matrix sm on e.id = sm.evaluation_id
                where e.id::text = :Id
                order by sm.created_date";
            }

            evaluation.SkillMatrices = await _context.QueryAsync<SkillMatrix>(sql.ToString(), new { request.Id, User = _user.UserName }, cancellationToken) as List<SkillMatrix>;


            return evaluation;
        }
    }
}
