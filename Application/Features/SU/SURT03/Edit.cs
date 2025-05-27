using Application.Behaviors;
using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.SURT03;

public class Edit
{
    public class Command : Profile, ICommand<Response>
    {

    }

    public class Response
    {
        public string ProfileCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            List<ProfileMenu> profileMenus = await _context.Set<ProfileMenu>().Where(w => w.ProfileCode == request.ProfileCode).ToListAsync();
            _context.Set<ProfileMenu>().RemoveRange(profileMenus);
            await _context.SaveChangesAsync(cancellationToken);

            if (_context.Set<Profile>().Any(a => a.ProfileCode == request.ProfileCode))
            {
                _context.Set<Profile>().Attach((Profile)request);
                _context.Entry((Profile)request).State = EntityState.Modified;
            }
            else
            {
                _context.Set<Profile>().Add((Profile)request);
            }

            _context.Set<ProfileMenu>().AddRange(request.ProfileMenus);
            await _context.SaveChangesAsync(cancellationToken);


            return new Response { ProfileCode = request.ProfileCode };
        }
    }
}
