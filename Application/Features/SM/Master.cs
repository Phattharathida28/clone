using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SM
{
    public class Master
    {
        public class Query : IRequest<MasterDTO>
        {

        }
        public class MasterDTO
        {
            public IEnumerable<dynamic> Projects { get; set; }
        }

        public class Handler : IRequestHandler<Query, MasterDTO>
        {
            private readonly ICleanDbContext _context;
            private readonly ICurrentUserAccessor _user;

            public Handler(ICleanDbContext context, ICurrentUserAccessor user)
            {
                _context = context;
                _user = user;
            }

            public async Task<MasterDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                MasterDTO masterDTO = new MasterDTO();

                StringBuilder sql = new StringBuilder();
                sql.AppendLine(@"
                    select 
                            t.space_name as value , 
                            t.folder_name as label
                    from clickup.tasks t
                    group by t.space_name , t.folder_name 
                ");
                masterDTO.Projects = await _context.QueryAsync<dynamic>(sql.ToString() , new {} , cancellationToken);
                return masterDTO;
            }
        }
    }
}
