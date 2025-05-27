using Application.Behaviors;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities.SU;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.SU.SURT01;

public class Save
{
    public class Command : Program, ICommand<Program>
    {

    }

    public class Handler : IRequestHandler<Command, Program>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Program> Handle(Command request, CancellationToken cancellationToken)
        {
            Validate(request);

            if (request.ProgramLabels == null) request.ProgramLabels = new List<ProgramLabel>();

            if (request.RowState == RowState.Add)
            {
                _context.Set<Program>().Add(request);
            }
            else if (request.RowState == RowState.Edit)
            {
                _context.Set<Program>().Attach(request);
                _context.Entry(request).State = EntityState.Modified;
            }
            else if (request.RowState == RowState.Delete)
            {
                _context.Set<Program>().Remove(request);
            }
            _context.Set<ProgramLabel>().RemoveRange(request.ProgramLabels.Where(w => w.RowState == RowState.Delete));
            _context.Set<ProgramLabel>().AddRange(request.ProgramLabels.Where(w => w.RowState == RowState.Add));
            _context.Set<ProgramLabel>().AttachRange(request.ProgramLabels.Where(w => w.RowState == RowState.Edit));

            request.ProgramLabels.Where(w => w.RowState == RowState.Edit).ToList().ForEach(f => _context.Entry(f).State = EntityState.Modified);
            await _context.SaveChangesAsync(cancellationToken);

            return request;
        }

        private void Validate(Program program)
        {
            List<ProgramLabel> suProgramLabels = program.ProgramLabels.Where(w => w.RowState == RowState.Add).ToList();

            if (_context.Set<Program>().Any(a => program.RowState == RowState.Add && a.ProgramCode == program.ProgramCode)) throw new RestException(HttpStatusCode.BadRequest, "message.STD00018", program.ProgramCode);
            else if (_context.Set<ProgramLabel>().Any(a => suProgramLabels.Select(s => s.FieldName).Contains(a.FieldName) && a.ProgramCode == program.ProgramCode && suProgramLabels.Select(s => s.LanguageCode).Contains(a.LanguageCode))) throw new RestException(HttpStatusCode.BadRequest, "message.STD00045");
        }
    }
}
