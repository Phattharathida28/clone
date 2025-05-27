using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.Menu;

public class List
{
    public class MenuVm
    {
        public string MenuCode { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public string RouterLinkTemp { get; set; }
        public List<string> RouterLink { get; set; }
        public string MainMenu { get; set; }
        public List<MenuVm> Items { get; set; }
    }
    public class Query : IRequest<IEnumerable<MenuVm>>
    {
    }
    public class Handler : IRequestHandler<Query, IEnumerable<MenuVm>>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }
        public async Task<IEnumerable<MenuVm>> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"
                select 
                    m.menu_code as ""MenuCode"",
                    concat('menu.',m.menu_code) as ""label"",
                    p.program_path as ""routerLinkTemp"",
                    m.main_menu as ""MainMenu"",
                    m.icon
                from su.menu m
                left join su.program p on p.program_code = m.program_code
                where m.system_code = 'ccs' and m.active = true
                and  exists(select 'x'	
	                             from su.profile_menu mp 	
	                             inner join su.user_profile p on p.profile_code  = mp.profile_code 	
	                             where mp.menu_code  = m.menu_code 	
	                             and p.user_id = :UserId ) 	
                order by m.menu_code
            ");

            IEnumerable<MenuVm> menus = await _context.QueryAsync<MenuVm>(sql.ToString(), new
            {
                UserId = _user.UserId
            }, cancellationToken);
            MenuVm root = new();
            menus.ToList().ForEach(f =>
            {
                if (!string.IsNullOrWhiteSpace(f.RouterLinkTemp))
                {
                    f.RouterLink = new();
                    f.RouterLink.Add(f.RouterLinkTemp);
                }
            });
            this.GetMenus(root, menus, null);
            return root.Items;
        }

        public void GetMenus(MenuVm parent, IEnumerable<MenuVm> menus, string menuCode)
        {
            IEnumerable<MenuVm> childs = menus.Where(o => o.MainMenu == menuCode);

            if (parent.Items == null && childs.Count() != 0)
            {
                parent.Items = new List<MenuVm>();
                parent.Items.AddRange(childs);
                foreach (var child in parent.Items) this.GetMenus(child, menus, child.MenuCode);
            }
        }
    }
}