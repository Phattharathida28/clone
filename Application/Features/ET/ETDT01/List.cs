using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities.ET;
using System;

namespace Application.Features.ET.ETDT01;

public class List
{
    public class Query : IRequest<List<EvaluationDTO>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
    }

    public class EvaluationDTO : Evaluation
    {
        public string StatusName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
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

            string employeeCode = await _context.ExecuteScalarAsync<string>("select e.employee_code from db.employee e inner join su.user u on u.user_id = e.user_id where u.user_name = :User", new { User = _user.UserName }, cancellationToken);
            string condition = string.Empty;

            if (new List<string> { "58034", "64028", "66001", "67003", "67019", "67022", "67031" }.Contains(employeeCode))
            {
                condition = $"and e.employee_code in ('{employeeCode}', 'SS67024', 'SS67044', 'SS67045')";
            }
            else if (employeeCode == "61047")
            {
                condition = "and e.employee_code in ('61047', '65006', '66001', '67022', 'SS67024', 'SS67044', 'SS67045')";
            }
            else if (employeeCode == "62010")
            {
                condition = "and e.employee_code in ('62010', '65011', '65074', '65075', '67003', '67019', '67031', 'SS67024', 'SS67044', 'SS67045')";
            }
            else if (employeeCode == "65011")
            {
                condition = "and e.employee_code in ('65011', '67003', 'SS67024', 'SS67044', 'SS67045')";
            }
            else if (employeeCode == "65006")
            {
                condition = "and e.employee_code in ('65006', '67022', 'SS67024', 'SS67044', 'SS67045')";
            }
            else if (employeeCode == "65074")
            {
                condition = "and e.employee_code in ('65074', '67019', 'SS67024', 'SS67044', 'SS67045')";
            }
            else if (employeeCode == "65075")
            {
                condition = "and e.employee_code in ('65075', '67031', 'SS67024', 'SS67044', 'SS67045')";
            }
            else if (employeeCode == "46028")
            {
                condition = "";
            }
            else
            {
                condition = $"and e.employee_code = '{employeeCode}'";
            }


            sql.AppendLine(@$"
            select 
	            e.id,
	            e.start_date ""startDate"",
	            e.end_date ""endDate"",
	            sl.""name"" ""statusName"",
                el.first_name || ' ' || el.last_name || case when el.nickname is not null then concat(' (', el.nickname, ')') else '' end  ""fullName"",
                u.user_name ""userName""
            from et.evaluation e 
            inner join db.employee e2 on e.employee_code = e2.employee_code 
            inner join db.employee_lang el on e2.employee_code = el.employee_code and el.language_code = upper(:lang)
            inner join su.user u on u.user_id = e2.user_id
            inner join db.status s on s.table_name = 'evaluation' and s.column_name = 'status' and s.id = e.status
            inner join db.status_lang sl on sl.language_code = upper(:lang) and sl.status_id = s.id
            where e.status::text = coalesce(:Status::uuid, e.status)::text and e.start_date::date = coalesce(:StartDate, e.start_date)::date and e.end_date::date = coalesce(:EndDate, e.end_date)::date {condition}
            order by ""startDate"" desc, ""endDate"" desc, s.seq");

            return await _context.QueryAsync<EvaluationDTO>(sql.ToString(), new { lang = _user.Language, request.StartDate, request.EndDate, request.Status }, cancellationToken) as List<EvaluationDTO>;
        }
    }
}
