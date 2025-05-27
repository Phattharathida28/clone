using Application.Behaviors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities.SU;
using System.Linq;

namespace Application.Features.SU.Setting;

public class Edit
{
    public class Command : ICommand<bool>
    {
        public bool? Ripple { get; set; }
        public string InputStyle { get; set; }
        public string MenuMode { get; set; }
        public string ColorScheme { get; set; }
        public string Color { get; set; }
        public int? Scale { get; set; }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            User user = _context.Set<User>().Where(w => w.UserName == _user.UserName).FirstOrDefault();

            if (user != null)
            {
                user.Ripple = request.Ripple;
                user.Inputstyle = request.InputStyle;
                user.Colorscheme = request.ColorScheme;
                user.Color = request.Color;
                user.Scale = request.Scale;
                user.Menumode = request.MenuMode;

                _context.Set<User>().Attach(user);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }
    }
}
