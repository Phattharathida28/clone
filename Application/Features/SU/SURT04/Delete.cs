using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Behaviors;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SU.SURT04;

public class Delete
{
    public class Command : ICommand
    {
        public long UserId { get; set; }
    }
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            User user = await _context.Set<User>().Where(w => w.Id == request.UserId).Include(i => i.UserProfiles).FirstOrDefaultAsync();
            _context.Set<User>().RemoveRange(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}