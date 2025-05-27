using Application.Interfaces;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Behaviors;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SU.SURT02;

public class Delete
{
    public class Command : ICommand
    {
        public string MenuCode { get; set; }
    }
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            Domain.Entities.SU.Menu suMenu = await _context.Set<Domain.Entities.SU.Menu>().Where(w => w.MenuCode == request.MenuCode).Include(i => i.MenuLabels).FirstOrDefaultAsync();
            _context.Set<Domain.Entities.SU.Menu>().RemoveRange(suMenu);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
