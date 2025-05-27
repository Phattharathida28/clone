using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Behaviors;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SU.SURT01;

public class Delete
{
    public class Command : ICommand
    {
        public string ProgramCode { get; set; }
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
            Program program = await _context.Set<Program>().Where(w => w.ProgramCode == request.ProgramCode).Include(i => i.ProgramLabels).FirstOrDefaultAsync();
            _context.Set<Program>().RemoveRange(program);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
