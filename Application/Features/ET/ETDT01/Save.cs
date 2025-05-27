using Application.Behaviors;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities.ET;
using System.Linq;
using System;
using System.Collections.Generic;
using Application.Exceptions;
using System.Net;

namespace Application.Features.ET.ETDT01;

public class Save
{
    public class Command : Evaluation, ICommand<Evaluation>
    {
        public bool Complete { get; set; }
    }

    public class MD
    {
        public decimal Points { get; set; } = 0;
        public decimal Performances { get; set; } = 0;
    }

    public class Handler : IRequestHandler<Command, Evaluation>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Evaluation> Handle(Command request, CancellationToken cancellationToken)
        {
            MD Md = await CalPoints(request.StartDate.Value, request.EndDate.Value, cancellationToken);
            string employeeCode = await _context.ExecuteScalarAsync<string>("select e.employee_code from db.employee e inner join su.user u on u.user_id = e.user_id where u.user_name = :User", new { User = _user.UserName }, cancellationToken);

            if (request.RowState == RowState.Add)
            {
                if (_context.Set<Evaluation>().Any(a => a.StartDate <= request.StartDate && a.EndDate >= request.EndDate && a.EmployeeCode == employeeCode))
                    throw new RestException(HttpStatusCode.BadRequest, "message.STD00018");
                else
                {
                    request.EmployeeCode = await _context.ExecuteScalarAsync<string>(@"select e.employee_code from su.""user"" u inner join db.employee e on u.user_id = e.user_id where u.user_name = :User", new { User = _user.UserName }, cancellationToken);
                    request.Status = await _context.ExecuteScalarAsync<Guid>(@"select s.id from db.status s where s.table_name = 'evaluation' and s.column_name = 'status' and s.seq = 0", new { }, cancellationToken);

                    request.EvaluationDetails.ToList().ForEach(f =>
                    {
                        if (f.Subject == "ปริมาณงาน") f.Score = Md.Points;
                        else if (f.Subject == "คุณภาพงาน") f.Score = Md.Performances;
                    });

                    _context.Set<Evaluation>().Add(request);
                }
            }
            else if (request.RowState == RowState.Edit)
            {
                request.EvaluationDetails.ToList().ForEach(f =>
                {
                    if (f.Subject == "ปริมาณงาน") f.Score = Md.Points;
                    else if (f.Subject == "คุณภาพงาน") f.Score = Md.Performances;
                });

                if (request.Complete)
                {
                    request.Status = await _context.ExecuteScalarAsync<Guid>(@"select s.id from db.status s where s.table_name = 'evaluation' and s.column_name = 'status' and s.seq = 10", new { }, cancellationToken);
                }

                _context.Set<Evaluation>().Attach(request);
                _context.Entry(request).State = EntityState.Modified;
            }

            _context.Set<EvaluationDetail>().AttachRange(request.EvaluationDetails.Where(w => w.RowState == RowState.Edit));
            request.EvaluationDetails.Where(w => w.RowState == RowState.Edit).ToList().ForEach(f => _context.Entry(f).State = EntityState.Modified);
            _context.Set<SkillMatrix>().AttachRange(request.SkillMatrices.Where(w => w.RowState == RowState.Edit));
            request.SkillMatrices.Where(w => w.RowState == RowState.Edit).ToList().ForEach(f => _context.Entry(f).State = EntityState.Modified);

            await _context.SaveChangesAsync(cancellationToken);

            return request;
        }

        private async Task<MD> CalPoints(DateTime StartDate, DateTime EndDate, CancellationToken cancellationToken)
        {
            MD Md = await _context.QueryFirstOrDefaultAsync<MD>(@"
            with recursive task_hierarchy as (
                select
                    t.task_id,
                    t.parent_task_id,
                    t.task_type,
                    t.""name"",
                    t.username,
                    t.space_name,
                    t.folder_name,
                    t.list_name,
                    t.status,
                    t.start_date,
                    t.end_date,
                    t.points,
                    t.""name"" as root_task_name,
                    t.task_id as root_task_id,
                    1 AS level
                from clickup.tasks t
                where t.parent_task_id is null
                union all
                select 
                    t.task_id,
                    t.parent_task_id,
                    t.task_type,
                    t.""name"",
                    t.username,
                    t.space_name,
                    t.folder_name,
                    t.list_name,
                    t.status,
                    t.start_date,
                    t.end_date,
                    t.points,
                    th.root_task_name,
                    th.root_task_id,
                    th.level + 1
                from clickup.tasks t
                inner join task_hierarchy th on t.parent_task_id = th.task_id
            )
            select
                coalesce(sum(th.points) filter(where (
                    (e.position_code IN ('SD', 'SSD', 'PCO', 'PCX', 'ITS') AND lower(root_tasks.""name"") NOT IN ('meeting', 'issue') and th.space_name != 'SS-OtherTasks' and th.folder_name != 'Training')
                    OR (e.position_code IN ('SA', 'ASA') AND lower(root_tasks.""name"") NOT IN ('meeting', 'issue', 'development') and th.space_name != 'SS-OtherTasks' and th.folder_name != 'Training')
                    or (lower(th.task_type) = 'trainer' and th.space_name = 'SS-OtherTasks' and th.folder_name = 'Training')
                )), 0) ""points"",
                coalesce(sum(th.points) filter(where lower(root_tasks.""name"") = 'issue'), 0) ""performances""
            from task_hierarchy th
            inner join clickup.tasks root_tasks ON th.root_task_id = root_tasks.task_id
            inner join db.employee e on e.clickup_user = th.username
            inner join su.""user"" u on u.user_id = e.user_id 
            where th.space_name not in ('SS-Meeting') 
	            and th.folder_name not in('Other')
                and th.list_name not in ('Other Task & Self-Learning') 
                and th.status = 'closed' 
                and th.start_date::date >= :StartDate
                and th.end_date::date <= :EndDate
                and u.user_name = :user
            ",
            new
            {
                User = _user.UserName,
                StartDate,
                EndDate
            },
            cancellationToken) ?? new();

            string PositionCode = await _context.ExecuteScalarAsync<string>(@"select e.position_code from db.employee e inner join su.""user"" u on e.user_id = u.user_id where u.user_name = :User", new { User = _user.UserName }, cancellationToken);

            int MdOfMonth = 0;
            int YearDifference = EndDate.Year - StartDate.Year;
            int MonthDifference = EndDate.Month - StartDate.Month;
            int Month = (YearDifference * 12) + MonthDifference;
            Month += 1;

            if (PositionCode == "SD" || PositionCode == "PCO") MdOfMonth = 16;
            else if (PositionCode == "SSD") MdOfMonth = 14;
            else if (PositionCode == "ASA") MdOfMonth = 13;
            else if (PositionCode == "SA" || PositionCode == "PCX") MdOfMonth = 12;
            else MdOfMonth = 10;

            Md.Performances = Md.Points == 0 ? 0 : 10 - ((Md.Performances / Md.Points) * 10);
            Md.Points = (Md.Points / (Month * MdOfMonth)) * (PositionCode == "ITS" ? 30 : 20);

            return Md;
        }
    }
}
