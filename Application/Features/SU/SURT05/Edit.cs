using Application.Behaviors;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.SU;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.SURT05;
public class Edit
{
    public class Command : MessageDTO, ICommand<Response>
    {
    }
    public class MessageDTO : Message
    {
        public string MessageCodeTh { get; set; }
        public string MessageCodeEn { get; set; }
    }
    public class Response
    {
        public string MessageCode { get; set; }
    }
    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {

            

            Message messageTH = await _context.Set<Message>().Where(w => w.MessageCode == request.MessageCode && w.LanguageCode == "TH").FirstOrDefaultAsync();
            Message messageEN = await _context.Set<Message>().Where(w => w.MessageCode == request.MessageCode && w.LanguageCode == "EN").FirstOrDefaultAsync();
            List<Message> messages = new List<Message>();
            if (messageTH != null && messageEN != null)
            {
                messageTH.MessageDesc = request.MessageCodeTh;
                messageTH.Remark = request.Remark;
                messageTH.RowState = RowState.Edit;
                messages.Add(messageTH);

                messageEN.MessageDesc = request.MessageCodeEn;
                messageEN.Remark = request.Remark;
                messageEN.RowState = RowState.Edit;
                messages.Add(messageEN);

                _context.Set<Message>().AttachRange(messages);
                messages.ForEach(f => _context.Entry(f).State = EntityState.Modified);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new Response { MessageCode = request.MessageCode };
        }
    }
}
