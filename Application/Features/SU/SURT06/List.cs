using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.SURT06;

public class List
{
    public class Query : IRequest<List<Domain.Entities.SU.Parameter>>
    {
        public string Keywords { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<Domain.Entities.SU.Parameter>>
    {
        private readonly ICleanDbContext _context;

        public Handler(ICleanDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.SU.Parameter>> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"
                                select 
                                    p.parameter_group_code ""parameterGroupCode"", 
                                    p.parameter_code ""parameterCode"" , 
                                    p.parameter_value ""parameterValue"" , 
                                    p.remark,
                                    p.xmin ""rowVersion""
                                from su.""parameter"" p 
                                where 1 = 1
                ");

            if (!string.IsNullOrEmpty(request.Keywords)) sql.AppendLine("and lower(concat(p.parameter_group_code,p.parameter_code,p.parameter_value,p.remark )) like lower(concat('%' , :Keywords , '%'))");


            return await _context.QueryAsync<Domain.Entities.SU.Parameter>(sql.ToString(), new { Keywords = request.Keywords }, cancellationToken) as List<Domain.Entities.SU.Parameter>;
        }
    }
}

