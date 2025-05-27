using Application.Interfaces;
using MediatR;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities.DB;
using System.Collections.Generic;
using Domain.Entities.SU;

namespace Application.Features.SU.SURT02;

public class Detail
{
    public class Query : IRequest<Domain.Entities.SU.Menu>
    {
        public string MenuCode { get; set; }
    }

    public class Handler : IRequestHandler<Query, Domain.Entities.SU.Menu>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;
        public async Task<Domain.Entities.SU.Menu> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            Domain.Entities.SU.Menu menu = new();

            sql.AppendLine(@"select  
	                            m.menu_code ""menuCode"",
	                            m.main_menu ""mainMenu"",
	                            m.program_code ""programCode"",
	                            m.icon,
	                            m.active,
	                            m.xmin ""rowVersion""
                            from su.menu m 
                            where m.menu_code = @menuCode"
            );

            menu = await _context.QueryFirstOrDefaultAsync<Domain.Entities.SU.Menu>(sql.ToString(), new { request.MenuCode }, cancellationToken) ?? new();

            sql = new();
            sql.AppendLine(@"select 
	                            ml.language_code ""languageCode"", 
	                            ml.menu_code ""menuCode"",
	                            ml.menu_name ""menuName"",
	                            l.description ""language"",
	                            ml.xmin ""rowVersion""
                             from su.menu_label ml 
                             left join db.language l on ml.language_code = l.language_code
                             where ml.menu_code = @MenuCode");

            menu.MenuLabels = (List<MenuLabel>)await _context.QueryAsync<MenuLabel>(sql.ToString(), new { request.MenuCode }, cancellationToken);

            return menu;

        }
    }
}
