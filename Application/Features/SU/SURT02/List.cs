using Application.Interfaces;
using MediatR;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Application.Features.SU.SURT02;

public class List
{
    public class Query : IRequest<List<MenuDTO>>
    {
        public string Keywords { get; set; }
    }

    public class MenuDTO: Domain.Entities.SU.Menu
    {
        public string MenuName { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<MenuDTO>>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }

        public async Task<List<MenuDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"
                                select * from(
                                select 
                                        sm.menu_code ""menuCode"",
                                        sm.main_menu ""mainMenu"",
                                        sm.program_code ""programCode"", 
                                        sm.active, 
                                        (select sml.menu_name where lower(sml.language_code) = lower(@Lang)) as ""menuName""
                                from su.menu sm 
                                inner join su.menu_label sml on sm.menu_code = sml.menu_code) as tmp
                                where ""menuName"" is not null
                                ");

            if (!string.IsNullOrEmpty(request.Keywords)) sql.AppendLine("and lower(concat(\"menuCode\",\"mainMenu\",\"programCode\", \"menuName\")) ilike lower(concat('%',@Keywords,'%'))");

            sql.AppendLine("order by \"menuCode\"");

            return await _context.QueryAsync<MenuDTO>(sql.ToString(), new { Lang = _user.Language, request.Keywords }, cancellationToken) as List<MenuDTO>;
        }
    }
}