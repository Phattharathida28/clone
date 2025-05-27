using Application.Features.SM;
using Application.Interfaces;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Controllers.SM
{

    public class Smrp01Controller : BaseController
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;

        public Smrp01Controller(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }
        public class TasksDTO
        {
            public string Month { get; set; }
            public double PmPoint { get; set; }
            public double SaPoint { get; set; }
            public double SdPoint { get; set; }
            public double TstPoint { get; set; }
            public double Total { get; set; }
            public int Investment { get; set; }
        }

        public class ExportExcelDTO
        {
            public string ProjectName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> ExportExcel([FromBody]  ExportExcelDTO request , CancellationToken cancellationToken)
        {
            DataTable dt = new();
            dt.TableName = "ARMS Timesheets";
            dt.Columns.Add("Month", typeof(string));
            dt.Columns.Add("PM", typeof(double));
            dt.Columns.Add("SA", typeof(double));
            dt.Columns.Add("SD", typeof(double));
            dt.Columns.Add("TST", typeof(double));
            dt.Columns.Add("Total", typeof(double));
            dt.Columns.Add("Investment", typeof(int));

            DateTime startDate = DateTime.SpecifyKind(request.StartDate , DateTimeKind.Utc);
            DateTime endDate = DateTime.SpecifyKind(request.EndDate , DateTimeKind.Utc);

            StringBuilder sql = new();
            sql.AppendLine(@"
                    select 
                        replace(date_part('year' , t.date_closed) || ',' || TO_CHAR(t.date_closed ,'MONTH')||'-'|| date_part('day' , t.date_closed)  , ' ' , '') ""month"" , 
                        coalesce(pm.points , 0) ""pmPoint"" , 
                        coalesce(sa.points , 0) ""saPoint"" , 
                        coalesce(sd.points , 0) ""sdPoint"" , 
                        coalesce(tst.points , 0) ""tstPoint""
                from clickup.tasks t
                left join (
	                select ct.date_closed::date ,  sum(ct.points) as points
	                from clickup.tasks t
	                inner join clickup.tasks ct on ct.parent_task_id = t.task_id  
	                where t.task_type = 'Milestone' and t.name = 'Project Management' 
	                and t.space_name = @ProjectName
	                and ct.date_closed::date between @StartDate::date and @EndDate::date
	                group by ct.date_closed::date
                ) pm on t.date_closed::date = pm.date_closed
                left join (
	                select ct.date_closed::date ,  sum(ct.points) as points
	                from clickup.tasks t
	                inner join clickup.tasks ct on ct.parent_task_id = t.task_id  
	                where t.task_type = 'Milestone' and t.name = 'Analysis and Design' and t.space_name = @ProjectName and ct.date_closed::date between @StartDate::date and @EndDate::date
	                group by ct.date_closed::date
                ) sa on t.date_closed::date = sa.date_closed
                left join (
	                select ct.date_closed::date ,  sum(ct.points) as points
	                from clickup.tasks t
	                inner join clickup.tasks ct on ct.parent_task_id = t.task_id  
	                where t.task_type = 'Milestone' and t.name = 'Development' and t.space_name = @ProjectName and ct.date_closed::date between @StartDate::date and @EndDate::date
	                group by ct.date_closed::date
                ) sd on t.date_closed::date = sd.date_closed
                left join (
	                select ct.date_closed::date ,  sum(ct.points) as points
	                from clickup.tasks t
	                inner join clickup.tasks ct on ct.parent_task_id = t.task_id  
	                where t.task_type = 'Milestone' and t.name = 'Test' and t.space_name = @ProjectName and ct.date_closed::date between @StartDate::date and @EndDate::date
	                group by ct.date_closed::date
                ) tst on t.date_closed::date = tst.date_closed
                where t.task_type = 'Milestone' and t.date_closed is not null and t.date_closed::date between @StartDate::date and @EndDate::date
                group by ""month"" , pm.points , sa.points , sd.points , tst.points
                ");
            List<TasksDTO> tasksDTOs = (List<TasksDTO>)await _context.QueryAsync<TasksDTO>(sql.ToString(), new { ProjectName = request.ProjectName  , StartDate = startDate, EndDate = endDate}, cancellationToken);

            string checkYear = "";

            foreach (TasksDTO taskDTO in tasksDTOs)
            {

                if (checkYear != taskDTO.Month.Split(',')[0])
                {
                    checkYear = taskDTO.Month.Split(',')[0];
                    dt.Rows.Add(taskDTO.Month.Split(',')[0]);
                }
                dt.Rows.Add(taskDTO.Month.Split(',')[1], taskDTO.PmPoint , taskDTO.SaPoint , taskDTO.SdPoint , taskDTO.TstPoint , 0, 0);
            }
            using (XLWorkbook wb = new XLWorkbook()){
                IXLWorksheet sheet = wb.AddWorksheet(dt, "ARMS Timesheets");
                sheet.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.GreenPigment;
                sheet.Row(1).CellsUsed().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                using(MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    string filename = $"timesheets_{DateTime.Now}.xlsx";
                    return Ok(File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename));
                }
            }
            
        }

        [HttpPost ("list")]
        public async Task<ActionResult> List([FromBody] List.Command command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet ("master")]
        public async Task<ActionResult> Master([FromQuery] Master.Query query)
        {
            return Ok(await Mediator.Send(query));
        }

    }
}
