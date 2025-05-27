using Application.Behaviors;
using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.SURT05;
public class Delete
{
    public class Command : ICommand
    {
        public string MessageCode { get; set; }
        public string LanguageCode { get; set; }
    }
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            List<Message> message = _context.Set<Message>().Where(w => w.MessageCode == request.MessageCode).ToList();
            _context.Set<Message>().RemoveRange(message);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}