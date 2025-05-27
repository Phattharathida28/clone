using Application.Behaviors;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.ET;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ET.ETRT02;

public class Save
{
    public class Command : SkillMatrixMaster, ICommand<SkillMatrixMaster>
    {

    }

    public class Handler : IRequestHandler<Command, SkillMatrixMaster>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<SkillMatrixMaster> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.RowState == RowState.Add)
            {
                if (_context.Set<SkillMatrixMaster>().Any(a => a.PositionCode == request.PositionCode))
                    throw new RestException(HttpStatusCode.BadRequest, "message.STD00018", request.PositionCode);
                _context.Set<SkillMatrixMaster>().Add(request);
            }
            else if (request.RowState == RowState.Edit)
            {
                _context.Set<SkillMatrixMaster>().Attach(request);
                _context.Entry(request).State = EntityState.Modified;
            }

            _context.Set<SkillMatrixMasterDetail>().RemoveRange(request.SkillMatrixMasterDetails.Where(w => w.RowState == RowState.Delete));
            _context.Set<SkillMatrixMasterDetail>().AddRange(request.SkillMatrixMasterDetails.Where(w => w.RowState == RowState.Add));
            _context.Set<SkillMatrixMasterDetail>().AttachRange(request.SkillMatrixMasterDetails.Where(w => w.RowState == RowState.Edit));

            request.SkillMatrixMasterDetails.Where(w => w.RowState == RowState.Edit).ToList().ForEach(f => _context.Entry(f).State = EntityState.Modified);
            await _context.SaveChangesAsync(cancellationToken);

            return request;
        }
    }
}
