using Application.Behaviors;
using Application.Interfaces;
using Domain.Entities.SU;
using Domain.Entities.TM;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using static Application.Features.TM.TMDT01.List;

namespace Application.Features.TM.TMDT01
{
    public class Delete
    {
        public class Command : MemberHierarchy
        {
            public int UserId { get; set; }
        }
        public class Handler : List<MemberHierarchy>, ICommand<Unit>
        {
            private readonly ICleanDbContext _context;
            public Handler(ICleanDbContext context) => _context = context;

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                MemberHierarchy MemberHierarchy = await _context.Set<MemberHierarchy>().Where(w => w.UserId == request.UserId).FirstOrDefaultAsync();
                _context.Set<MemberHierarchy>().RemoveRange(MemberHierarchy);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}