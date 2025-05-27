using Application.Behaviors;
using Application.Common.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.SURT05;

public class Create
{

    public class Command : MessageCodeDTO, ICommand<Response>
    {

    }

    public class MessageCodeDTO : Message
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
        private readonly ICurrentUserAccessor _user;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {   
            if (_context.Set<Domain.Entities.SU.Message>().Any(a => a.MessageCode == request.MessageCode)) throw new RestException(HttpStatusCode.BadRequest, "message.STD00018", request.MessageCode);

            if (request.RowVersion == null)
            {
                for (int i = 0; i < 2; i++)
                {
                    Message MessageLang = new Message();
                    MessageLang.MessageCode = request.MessageCode;
                    MessageLang.LanguageCode = i == 0 ? Lang.TH : Lang.EN;
                    MessageLang.MessageDesc = i == 0 ? request.MessageCodeTh : request.MessageCodeEn;
                    MessageLang.Remark = request.Remark;

                    _context.Set<Message>().Add(MessageLang);
                }
            }

            

            await _context.SaveChangesAsync(cancellationToken);

            return new Response { MessageCode = request.MessageCode };
        }
    }
}
