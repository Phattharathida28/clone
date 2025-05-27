using Application.Behaviors;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.ET;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ET.ETRT01;

public class Save
{
    public class Command : EvaluationMaster, ICommand<EvaluationMaster>
    {

    }

    public class Handler : IRequestHandler<Command, EvaluationMaster>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<EvaluationMaster> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.RowState == RowState.Add)
            {
                _context.Set<EvaluationMaster>().Add(request);
            }
            else if(request.RowState == RowState.Edit)
            {
                _context.Set<EvaluationMaster>().Attach(request);
                _context.Entry(request).State = EntityState.Modified;
            }

            _context.Set<EvaluationMasterDetail>().RemoveRange(request.EvaluationMasterDetails.Where(w => w.RowState == RowState.Delete));
            _context.Set<EvaluationMasterDetail>().AddRange(request.EvaluationMasterDetails.Where(w => w.RowState == RowState.Add));
            _context.Set<EvaluationMasterDetail>().AttachRange(request.EvaluationMasterDetails.Where(w => w.RowState == RowState.Edit));

            request.EvaluationMasterDetails.Where(w => w.RowState == RowState.Edit).ToList().ForEach(f => _context.Entry(f).State = EntityState.Modified);
            await _context.SaveChangesAsync(cancellationToken);

            return request;
        }
    }
}