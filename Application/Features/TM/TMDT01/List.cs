using Application.Interfaces;
using Domain.Entities.SU;
using Domain.Entities.TM;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TM.TMDT01
{
    public class List
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
                StringBuilder sql = new StringBuilder();
                //sql.AppendLine(@"
                //                select u.user_id ""userId"" ,u.firstname_th ""firstnameTh"" ,u.lastname_th ""lastnameTh"" ,  p.position_name_en ""positionNameEn""
                //                from db.employee e 
                //                left join su.""user"" u on e.user_id = u.user_id 
                //                left join db.""position"" p on p.position_code = e.position_code 
                //                where u.user_id not in (
                //                    select distinct u2.user_id from su.""user"" u2 
                //                 left join tm.""member_hierarchy"" mh2 on u2.user_id = mh2.user_id
                //                 where u2.user_id in (mh2.user_id)
                //                )");

                sql.AppendLine(@"select u.user_id ""userId"" ,
		                                u.firstname_th ""firstnameTh"" ,
		                                u.lastname_th ""lastnameTh"" , 
		                                p.position_code ""positionNameEn""
                                from su.""user"" u 
                                left join db.employee e on u.user_id = e.user_id 
                                left join db.""position"" p on p.position_code = e.position_code 
                                where u.user_id not in (
                                    select distinct u2.user_id from su.""user"" u2 
                                left join tm.""member_hierarchy"" mh2 on u2.user_id = mh2.user_id
                                    where u2.user_id in (mh2.user_id)
                                )");
                return await _context.QueryAsync<UserDTO>(sql.ToString(), null, cancellationToken) as List<UserDTO>;
            }
        }
    }
}