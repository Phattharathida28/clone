using Application.Behaviors;
using Application.Common.Constants;
using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.SU.SURT01;

public class Copy
{
    public class Command : ICommand<Response>
    {
        public string LangCode { get; set; }
        public string ProgramCode { get; set; }
    }

    public class Response
    {
        public string ProgramCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            List<ProgramLabel> copy = _context.Set<ProgramLabel>().Where(w => w.ProgramCode == request.ProgramCode && w.LanguageCode == request.LangCode).ToList();
            List<ProgramLabel> programLabels = _context.Set<ProgramLabel>().Where(w => w.ProgramCode == request.ProgramCode && w.LanguageCode != request.LangCode).ToList();
            ProgramLabel programLabel = new ProgramLabel();

            foreach (ProgramLabel item in copy)
            {
                if (!programLabels.Any(w => w.FieldName == item.FieldName))
                {
                    programLabel = new ProgramLabel();
                    programLabel.ProgramCode = item.ProgramCode;
                    programLabel.FieldName = item.FieldName;
                    programLabel.LanguageCode = request.LangCode == Lang.TH ? Lang.EN : Lang.TH;
                    programLabel.LabelName = item.LabelName;
                    programLabel.ModuleCode = item.ModuleCode;
                    programLabel.SystemCode = item.SystemCode;
                    _context.Set<ProgramLabel>().Add(programLabel);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new Response { ProgramCode = request.ProgramCode };
        }
    }
}
