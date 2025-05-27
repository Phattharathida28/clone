using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities.QO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.QORT01.Command
{
    public class Delete
    {
        public class Command : IRequest<decimal>
        {
            public int PLDetId { get; set; }
        }

        public class Handler : IRequestHandler<Command, decimal>
        {
            private readonly ICleanDbContext _context;

            public Handler(ICleanDbContext context)
            {
                _context = context;
            }

            public async Task<decimal> Handle(Command request, CancellationToken cancellationToken)
            {
                var detail = await _context.Set<QoPriceDetail>()
                    .FirstOrDefaultAsync(x => x.PlDetId == request.PLDetId, cancellationToken);

                if (detail == null) throw new Exception("ไม่พบข้อมูลสินค้าและบริการ");

                decimal PlId = detail.PlId;

                _context.Set<QoPriceDetail>().Remove(detail);
                await _context.SaveChangesAsync(cancellationToken);
                
                return PlId;
            }
        }
    }
}
