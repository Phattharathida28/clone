using Application.Behaviors;
using Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.SURT06;
public class Delete
{
    public class Command : ICommand
    {
        public string ParameterGroupCode { get; set; }
        public string ParameterCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            Domain.Entities.SU.Parameter parameter =  _context.Set<Domain.Entities.SU.Parameter>().Where(w => w.ParameterGroupCode == request.ParameterGroupCode && w.ParameterCode == request.ParameterCode).FirstOrDefault();
            _context.Set<Domain.Entities.SU.Parameter>().Remove(parameter);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
