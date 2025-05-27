using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Constants;

namespace Application.Features.SU.SURT02;

public class Master
{
    public class MasterData
    {
        public IEnumerable<dynamic> MainMenu { get; set; }
        public IEnumerable<dynamic> ProgramCode { get; set; }
        public IEnumerable<dynamic> Langs { get; set; }
    }

    public class Query : IRequest<MasterData>
    {

    }

    public class Handler : IRequestHandler<Query, MasterData>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<MasterData> Handle(Query request, CancellationToken cancellationToken)
        {
            MasterData master = new MasterData();
            master.MainMenu = await GetMainMenu(cancellationToken);
            master.ProgramCode = await GetProgramCode(cancellationToken);
            master.Langs = await GetLangs(cancellationToken);   
            return master;
        }
        private async Task<IEnumerable<dynamic>> GetMainMenu(CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"select sm.menu_code as value, concat(sm.menu_code,' : ', (select sml.menu_name from su.menu_label sml where sm.menu_code = sml.menu_code and lower(sml.language_code) = lower(@lang))) as label
                                from su.menu sm 
                                order by sm.menu_code"
            );

            return await _context.QueryAsync<dynamic>(sql.ToString(), new { lang = _user.Language }, cancellationToken);
        }

        private async Task<IEnumerable<dynamic>> GetProgramCode(CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"select sp.program_code as value ,sp.program_code || ' : ' || sp.program_name as label from su.program sp order by sp.program_code");

            return await _context.QueryAsync<dynamic>(sql.ToString(), new { }, cancellationToken);
        }
        private async Task<IEnumerable<dynamic>> GetLangs(CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"select 
                l.language_code ""value"", case when ll.language_name is null then l.language_code else ll.language_name end ""label"", l.pattern
            from db.""language"" l 
            left join db.language_lang ll on l.language_code = ll.language_code and ll.language_code_forname = upper(@Lang)
            where l.active = true
            order by l.language_code, ll.language_name");

            return await _context.QueryAsync<dynamic>(sql.ToString(), new { Lang = _user.Language }, cancellationToken);
        }
    }
}