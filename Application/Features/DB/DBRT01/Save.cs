using Application.Behaviors;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.DB.DBRT01;

public class Save
{
    public class Command : Language, ICommand<Language> { }

    public class Handler : IRequestHandler<Command, Language>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Language> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.RowState == RowState.Add)
            {
                if (_context.Set<Language>().Any(w => w.LanguageCode == request.LanguageCode))
                    throw new RestException(HttpStatusCode.BadRequest, "message.STD00018", request.LanguageCode);
                else
                {
                    request.LanguageCode = request.LanguageCode.ToUpper();
                    _context.Set<Language>().Add(request);
                }
            }
            else if (request.RowState == RowState.Edit)
            {
                _context.Set<Language>().Attach(request);
                _context.Entry(request).State = EntityState.Modified;
            }

            _context.Set<LanguageLang>().AddRange(request.LanguageLangs.Where(w => w.RowState == RowState.Add));
            _context.Set<LanguageLang>().AttachRange(request.LanguageLangs.Where(w => w.RowState == RowState.Edit));

            request.LanguageLangs.Where(w => w.RowState == RowState.Edit).ToList().ForEach(f => _context.Entry(f).State = EntityState.Modified);
            await _context.SaveChangesAsync(cancellationToken);

            return request;
        }
    }
}