using Application.Behaviors;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities.DB;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.DB.DBRT02;

public class Save
{
    public class Command : ListItemGroup, ICommand<ListItemGroup> { }

    public class Handler : IRequestHandler<Command, ListItemGroup>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<ListItemGroup> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.RowState == RowState.Add)
            {
                if (_context.Set<ListItemGroup>().Any(w => w.ListItemGroupCode == request.ListItemGroupCode))
                    throw new RestException(HttpStatusCode.BadRequest, "message.STD00018", request.ListItemGroupCode);
                else
                {
                    request.SystemCode = "CCS";
                    _context.Set<ListItemGroup>().Add(request);
                }
            }
            else if (request.RowState == RowState.Edit)
            {
                _context.Set<ListItemGroup>().Attach(request);
                _context.Entry(request).State = EntityState.Modified;
            }

            if (request.ListItems != null)
            {
                _context.Set<ListItem>().AddRange(request.ListItems.Where(w => w.RowState == RowState.Add));
                _context.Set<ListItem>().AttachRange(request.ListItems.Where(w => w.RowState == RowState.Edit));
                request.ListItems.Where(w => w.RowState == RowState.Edit).ToList().ForEach(f => _context.Entry(f).State = EntityState.Modified);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return request;
        }
    }
}
