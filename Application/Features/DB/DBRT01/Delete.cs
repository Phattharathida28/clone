using Application.Interfaces;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities.DB;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Application.Behaviors;

namespace Application.Features.DB.DBRT01;

public class Delete
{
    public class Command : ICommand
    {
        public string LanguageCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            Language language = await _context.Set<Language>().Where(w => w.LanguageCode == request.LanguageCode).Include(i => i.LanguageLangs).FirstOrDefaultAsync();
            _context.Set<Language>().RemoveRange(language);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
