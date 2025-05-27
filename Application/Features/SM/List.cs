using Application.Behaviors;
using Application.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities.ET;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Features.SM
{
    public class List
    {
        public class Command : SearchDTO, ICommand<List<TasksDTO>>
        {

        }

        public class SearchDTO
        {
            public string ProjectName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class TasksDTO
        {
            public DateTime? Date { get; set; }
            public double PmPoint { get; set; }
            public string PmNames { get; set; }
            public double SaPoint { get; set; }
            public string SaNames { get; set; }
            public double SdPoint { get; set; }
            public string SdNames { get; set; }
            public double TstPoint { get; set; }
            public string TstNames { get; set; }
            public double Total { get; set; }
            public int Investment { get; set; }
        }


        public class Handler : IRequestHandler<Command, List<TasksDTO>>
        {
            private readonly ICleanDbContext _context;
            private readonly ICurrentUserAccessor _user;

            public Handler(ICleanDbContext context, ICurrentUserAccessor user)
            {
                _context = context;
                _user = user;
            }

            public async Task<List<TasksDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                StringBuilder sql = new StringBuilder();
                DateTime startDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
                DateTime endDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);
                sql.AppendLine(@" select 
                        t.date_closed::date ""date"" , 
                        coalesce(pm.points , 0) ""pmPoint"" ,
                        pm.names ""pmNames"",
                        coalesce(sa.points , 0) ""saPoint"" ,
                        sa.names ""saNames"",
                        coalesce(sd.points , 0) ""sdPoint"" , 
                        sd.names ""sdNames"",
                        coalesce(tst.points , 0) ""tstPoint"",
                        tst.names ""tstNames""
                from clickup.tasks t
                left join (
	                select ct.date_closed::date ,  sum(ct.points) as points , string_agg(ct.name || '?' || ct.points , '|') as names
	                from clickup.tasks t
	                inner join clickup.tasks ct on ct.parent_task_id = t.task_id  
	                where t.task_type = 'Milestone' and t.name = 'Project Management' 
	                and t.space_name = @ProjectName
	                and ct.date_closed::date between @StartDate::date and @EndDate::date
	                group by ct.date_closed::date
                ) pm on t.date_closed::date = pm.date_closed
                left join (
	                select ct.date_closed::date ,  sum(ct.points) as points , string_agg(ct.name || '?' || ct.points , '|') as names
	                from clickup.tasks t
	                inner join clickup.tasks ct on ct.parent_task_id = t.task_id  
	                where t.task_type = 'Milestone' and t.name = 'Analysis and Design' and t.space_name = @ProjectName and ct.date_closed::date between @StartDate::date and @EndDate::date
	                group by ct.date_closed::date
                ) sa on t.date_closed::date = sa.date_closed
                left join (
	                select ct.date_closed::date ,  sum(ct.points) as points , string_agg(ct.name || '?' || ct.points , '|') as names
	                from clickup.tasks t
	                inner join clickup.tasks ct on ct.parent_task_id = t.task_id  
	                where t.task_type = 'Milestone' and t.name = 'Development' and t.space_name = @ProjectName and ct.date_closed::date between @StartDate::date and @EndDate::date
	                group by ct.date_closed::date
                ) sd on t.date_closed::date = sd.date_closed
                left join (
	                select ct.date_closed::date ,  sum(ct.points) as points , string_agg(ct.name || '?' || ct.points , '|') as names
	                from clickup.tasks t
	                inner join clickup.tasks ct on ct.parent_task_id = t.task_id  
	                where t.task_type = 'Milestone' and t.name = 'Test' and t.space_name = @ProjectName and ct.date_closed::date between @StartDate::date and @EndDate::date
	                group by ct.date_closed::date
                ) tst on t.date_closed::date = tst.date_closed
                where t.task_type = 'Milestone' and t.date_closed is not null and t.date_closed::date between @StartDate::date and @EndDate::date
                group by t.date_closed::date , pm.points , sa.points , sd.points , tst.points , pm.names , sa.names , sd.names , tst.names");
                return await _context.QueryAsync<TasksDTO>(sql.ToString(), new { ProjectName = request.ProjectName, StartDate = startDate, EndDate = endDate }, cancellationToken) as List<TasksDTO>;
            }
        }
    }

}