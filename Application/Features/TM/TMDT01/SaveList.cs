using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TM.TMDT01
{
    public class SaveList
    {
        public class Query : IRequest<List<UserDTO>>
        {
            public string Keywords { get; set; }
        }
        public class UserDTO
        {
            public string PositionNameEn { get; set; }
            public int UserId { get; set; }
            public string FirstnameTh { get; set; }
            public string LastnameTh { get; set; }
            public int? HeadUser {  get; set; }
            //public string approve_by {  get; set; }
            //public DateTime? createDate {  get; set; }
            //public string employee_code { get; set; }
            //public string name { get; set; }
            //public string status { get; set; }
            //public string approverName { get; set; }

        }
        public class Handler : IRequestHandler<Query, List<UserDTO>>
        {
            private readonly ICleanDbContext _context;
            private readonly ICurrentUserAccessor _user;

            public Handler(ICleanDbContext context, ICurrentUserAccessor user)
            {
                _context = context;
                _user = user;
            }

            public async Task<List<UserDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                //StringBuilder sql1 = new StringBuilder();

                //sql1.AppendLine(@"select
	               //                 da.approve_by,
	               //                 da.created_date ""createDate"", 
	               //                 da.employee_code,
	               //                 case when @Lang = 'th' then concat(e.employee_firstname_th , ' ',e.employee_surname_th) else concat(e.employee_firstname_en , ' ',e.employee_surname_en) end ""name"",
	               //                 da.evaluation_status ""status"",
	               //                 case when @Lang = 'th' then concat(e2.employee_firstname_th , ' ',e2.employee_surname_th) else concat(e2.employee_firstname_en , ' ',e2.employee_surname_en) end ""approverName""
                //                from et.document_approved da 
                //                left join db.employee e on da.employee_code = e.employee_code
                //                left join tm.member_hierarchy mh on e.user_id = mh.user_id 
                //                left join db.employee e2 on da.approve_by = e2.employee_code");
                //var sql1Result = await _context.QueryAsync<UserDTO>(sql1.ToString(), new { Lang = _user.Language}, cancellationToken) as List<UserDTO>;
                //List<UserDTO> getApproveDoc = new List<UserDTO>();
                //List<UserDTO> getApproveDoc2 = new List<UserDTO>();
                //string userId = "99999";
                //foreach (var doc in sql1Result)
                //{
                //    if (userId == doc.approve_by && userId != doc.employee_code) getApproveDoc.Add(doc);
                //}
                //foreach (var doc in sql1Result)
                //{
                //    foreach(var getApprove in getApproveDoc)
                //    {
                //        if (userId != doc.employee_code && doc.approve_by == getApprove.employee_code) getApproveDoc2.Add(doc);
                //    }
                //}

                StringBuilder sql = new StringBuilder();
                sql.AppendLine(@" select      
                                tm.user_id ""userId"",
                                u.firstname_th ""firstnameTh"",
                                u.lastname_th ""lastnameTh"" ,
                                p.position_code ""positionNameEn"",
                                tm.head_user ""headUser"" 
                            from tm.""member_hierarchy"" tm 
                                left join su.""user"" u on tm.user_id = u.user_id 
                                left join db.""position"" p on p.position_code = (
	                                select e.position_code from db.employee e
	                                where e.user_id = tm.user_id)");

                return await _context.QueryAsync<UserDTO>(sql.ToString(), new { Lang = _user.Language, request.Keywords }, cancellationToken) as List<UserDTO>;
            }
        }
    }
}
