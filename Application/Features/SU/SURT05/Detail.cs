using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.SURT05;
public class Detail
{
    public class Query : IRequest<MessageDTO>
    {
        public string MessageCode { get; set; }
        public string LanguageCode { get; set; }
    }
    public class MessageDTO : Message
    {
        public string MessageCodeTh { get; set; }
        public string MessageCodeEn { get; set; }
    }

    public class Handler : IRequestHandler<Query, MessageDTO>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user) { 
            _context = context;
            _user = user;
        }
        public async Task<MessageDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"select m.message_code ""messageCode"",");

            if (_user.Language == "th") sql.AppendLine(@"(select m2.message_desc from su.message m2 where m2.message_code = m.message_code and m2.language_code = 'TH') as ""messageDesc"", ");
            else if (_user.Language == "en") sql.AppendLine(@"(select m3.message_desc from su.message m3 where m3.message_code = m.message_code and m3.language_code = 'EN') as ""messageDesc"", ");
            
            sql.AppendLine(@"m.language_code""languageCode"",
                                    m.remark,
                                    m.xmin ""rowVersion"",
                                    (select m2.message_desc from su.message m2 where m2.message_code = m.message_code and m2.language_code = 'TH') as ""messageCodeTh"", 
                                    (select m3.message_desc from su.message m3 where m3.message_code = m.message_code and m3.language_code = 'EN') as ""messageCodeEn""
                                    from su.message m
                                    where m.message_code = @messageCode");

            return await _context.QueryFirstOrDefaultAsync<MessageDTO>(sql.ToString(), new { messageCode = request.MessageCode }, cancellationToken);
        }
    }
}