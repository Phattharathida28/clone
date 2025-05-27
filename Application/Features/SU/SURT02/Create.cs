using Application.Behaviors;
using Application.Common.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.SU.SURT02;

public class Create
{

    public class Command : MenuDTO, ICommand<Response>
    {

    }

    public class MenuDTO : Domain.Entities.SU.Menu
    {
        public string MenuNameTH { get; set; }
        public string MenuNameEN { get; set; }
    }

    public class Response
    {
        public string MenuCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.RowState == Domain.Entities.RowState.Add)
            {
                if (_context.Set<Domain.Entities.SU.Menu>().Any(a => a.MenuCode == request.MenuCode)) 
                    throw new RestException(HttpStatusCode.BadRequest, "message.STD00018", request.MenuCode);
                    _context.Set<Domain.Entities.SU.Menu>().Add((Domain.Entities.SU.Menu)request);

                //for (int i = 0; i < 2; i++)
                //{
                //    MenuLabel menuLabel = new MenuLabel();
                //    menuLabel.MenuCode = request.MenuCode;
                //    menuLabel.LanguageCode = i == 0 ? Lang.TH : Lang.EN;
                //    menuLabel.SystemCode = "ccs";
                //    menuLabel.MenuName = i == 0 ? request.MenuNameTH : request.MenuNameEN;

                //    _context.Set<MenuLabel>().Add(menuLabel);
                //}
            }
            await _context.SaveChangesAsync(cancellationToken);

            return new Response { MenuCode = request.MenuCode };
        }
    }
}
