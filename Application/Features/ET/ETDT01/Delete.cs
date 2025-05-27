using Application.Interfaces;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Behaviors;
using Domain.Entities.ET;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ET.ETDT01;

public class Delete
{
    public class Command : ICommand
    {
        public string Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            Evaluation evaluation = await _context.Set<Evaluation>().Where(w => w.Id.ToString() == request.Id).Include(i => i.EvaluationDetails).Include(i => i.SkillMatrices).FirstOrDefaultAsync();
            _context.Set<Evaluation>().Remove(evaluation);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
