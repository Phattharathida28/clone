using Application.Interfaces;
using Domain.Entities.DB;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Behaviors;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DB.DBRT02;

public class Delete
{
    public class Command : ICommand
    {
        public string ListItemGroupCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            ListItemGroup language = await _context.Set<ListItemGroup>().Where(w => w.ListItemGroupCode == request.ListItemGroupCode).Include(i => i.ListItems).FirstOrDefaultAsync();
            _context.Set<ListItemGroup>().RemoveRange(language);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
