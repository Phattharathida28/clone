using Application.Behaviors;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Application.Exceptions;
using System.Linq;
using System.Net;

namespace Application.Features.SU.SURT06;
public class Save
{
    public class Command : Domain.Entities.SU.Parameter, ICommand<Domain.Entities.SU.Parameter> 
    { 

    }

    public class Handler : IRequestHandler<Command, Domain.Entities.SU.Parameter>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
        }
        public async Task<Domain.Entities.SU.Parameter> Handle(Command request, CancellationToken cancellationToken)
        {
            Validate(request);
            
            if (request.RowState == RowState.Add)
            {
                _context.Set<Domain.Entities.SU.Parameter>().Add(request);
            }
            else if (request.RowState == RowState.Edit)
            {
                _context.Set<Domain.Entities.SU.Parameter>().Attach(request);
                _context.Entry(request).State = EntityState.Modified;
            }
                await _context.SaveChangesAsync(cancellationToken);

            return request;
        }
        private void Validate(Domain.Entities.SU.Parameter parameter)
        {
            if (_context.Set<Domain.Entities.SU.Parameter>().Any(a => parameter.RowState == RowState.Add && a.ParameterGroupCode == parameter.ParameterGroupCode && a.ParameterCode == parameter.ParameterCode)) throw new RestException(HttpStatusCode.BadRequest, "message.STD00004", parameter.ParameterGroupCode);
        }
    }
}
