using Application.Behaviors;
using Application.Interfaces;
using Domain.Entities.ET;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ET.ETRT02;

public class Delete
{
    public class Command : ICommand
    {
        public string Id { get; set; }
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
            SkillMatrixMaster skillMatrixMaster = await _context.Set<SkillMatrixMaster>().Where(w => w.Id.ToString() == request.Id).Include(i => i.SkillMatrixMasterDetails).FirstOrDefaultAsync();
            _context.Set<SkillMatrixMaster>().RemoveRange(skillMatrixMaster);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
