using Application.Interfaces;
using Domain.Entities.DB;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.DB.DBRT01;

public class Detail
{
    public class Query : IRequest<Language>
    {
        public string LanguageCode { get; set; }
    }

    public class Handler : IRequestHandler<Query, Language>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;

        public async Task<Language> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            Language language = new();

            sql.AppendLine(@"
            select 
                l.language_code ""languageCode"", 
                l.description, l.active, l.pattern, l.xmin ""rowVersion"" 
            from db.""language"" l 
            where l.language_code = :LanguageCode");

            language = await _context.QueryFirstOrDefaultAsync<Language>(sql.ToString(), new { request.LanguageCode }, cancellationToken) ?? new();

            sql = new();
            sql.AppendLine(@"
            select 
	            ll.language_code ""languageCode"", 
	            ll.language_code_forname ""languageCodeForname"",
	            ll.language_name ""languageName"",
	            ll.xmin ""rowVersion""
            from db.language_lang ll
            where ll.language_code = :LanguageCode");

            language.LanguageLangs = (List<LanguageLang>)await _context.QueryAsync<LanguageLang>(sql.ToString(), new { request.LanguageCode }, cancellationToken);

            return language;
        }
    }
}
