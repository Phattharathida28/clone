using Application.Behaviors;
using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.SURT03;

public class Delete
{
    public class Command : ICommand
    {
        public string ProfileCode { get; set; }
    }
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            Profile profile = await _context.Set<Profile>().Where(w => w.ProfileCode == request.ProfileCode).Include(i => i.ProfileMenus).FirstOrDefaultAsync();
            _context.Set<Profile>().RemoveRange(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
