using Application.Interfaces;
using MediatR;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.Parameter;

public class Detail
{
    public class Query : IRequest<Object>
    {
        public string Group { get; set; }
        public string Code { get; set; }
    }
    public class Handler : IRequestHandler<Query, Object>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context)
        {
            _context = context;
        }
        public async Task<Object> Handle(Query request, CancellationToken cancellationToken)
        {
            var parameter = await _context.QuerySingleAsync<string>(@"SELECT json_object_agg(parameter_code, parameter_value) as Json  
                                                            FROM su.parameter sp   
                                                            where parameter_group_code = @Group 
                                                            and parameter_code = coalesce(@Code,sp.parameter_code)", new { Group = request.Group, Code = request.Code }, token: cancellationToken);
            return JsonDocument.Parse(parameter ?? "{}").RootElement;
        }
    }
}