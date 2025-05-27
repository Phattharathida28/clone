using Application.Behaviors;
using Application.Common.Constants;
using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.SU.SURT02;

public class Edit
{
    public class Command : Domain.Entities.SU.Menu, ICommand<Response> {}

     
    public class Response
    {
        public string MenuCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            _context.Set<Domain.Entities.SU.Menu>().Attach(request);
            _context.Entry(request).State = EntityState.Modified;

            List<MenuLabel> menuLabels = await _context.Set<MenuLabel>().Where(w => w.MenuCode == request.MenuCode).ToListAsync();
            List<MenuLabel> editMenuLabels = request.MenuLabels.ToList();

            foreach (MenuLabel menuLabel in menuLabels)
            {

                if(menuLabel.LanguageCode == Lang.EN)
                {
                    menuLabel.MenuName = editMenuLabels.FirstOrDefault(x => x.LanguageCode == Lang.EN).MenuName;
                }
                else
                {
                    menuLabel.MenuName = editMenuLabels.FirstOrDefault(x => x.LanguageCode == Lang.TH).MenuName;
                }

                _context.Set<MenuLabel>().Attach(menuLabel);
                _context.Entry(menuLabel).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new Response { MenuCode = request.MenuCode };
        }
    }
}
