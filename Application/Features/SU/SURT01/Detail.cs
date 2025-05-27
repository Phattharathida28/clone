using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.SU.SURT01;

public class Detail
{
    public class Query : IRequest<Program>
    {
        public string ProgramCode { get; set; }
    }

    public class Handler : IRequestHandler<Query, Program>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context)
        {
            _context = context;
        }
        public async Task<Program> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            Program program = new();

            sql.AppendLine(@"select p.program_code ""programCode"" , 
                                    p.program_name ""programName"", 
                                    p.program_path ""programPath"", 
                                    p.system_code ""systemCode"", 
                                    p.module_code ""moduleCode"",
                                    p.xmin ""rowVersion""
                                 from su.program p 
                                 where p.program_code = @programCode");

            program = await _context.QueryFirstOrDefaultAsync<Program>(sql.ToString(), new { programCode = request.ProgramCode }, cancellationToken);

            if (program != null)
            {
                sql = new StringBuilder();
                sql.AppendLine(@"select 
	                                pl.program_code ""programCode"",
	                                pl.field_name ""fieldName"",
	                                pl.label_name ""labelName"",
	                                pl.language_code ""languageCode"",
	                                pl.system_code ""systemCode"",
	                                pl.module_code  ""moduleCode"",
	                                pl.xmin ""rowVersion""
                                from su.program_label pl
                                where pl.program_code = @programCode");

                program.ProgramLabels = await _context.QueryAsync<ProgramLabel>(sql.ToString(), new { programCode = request.ProgramCode }, cancellationToken) as List<ProgramLabel>;

            }

            return program;
        }
    }
}