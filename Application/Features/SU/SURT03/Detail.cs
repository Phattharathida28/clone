using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SU.SURT03;

public class Detail
{
    public class Query : IRequest<ProfileDTO>
    {
        public string ProfileCode { get; set; }
    }

    public class ProfileDTO : Profile
    {
        public List<ProfileMenuDTO> ProfileMenus { get; set; }
    }

    public class ProfileMenuDTO : ProfileMenu
    {
        public string MenuName { get; set; }
        public string Icon { get; set; }
    }

    public class Handler : IRequestHandler<Query, ProfileDTO>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }
        public async Task<ProfileDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            ProfileDTO profile = new ProfileDTO();

            sql.AppendLine(@"select 
	                                profile_code ""profileCode"",
	                                description ,
	                                active ,
                                    xmin ""rowVersion""
                                from su.profile p 
                                where profile_code = @profileCode
                                order by profile_code");

            profile = await _context.QueryFirstOrDefaultAsync<ProfileDTO>(sql.ToString(), new { profileCode = request.ProfileCode }, cancellationToken);

            if (profile != null)
            {
                sql = new StringBuilder();
                sql.AppendLine(@"select 
                                        pm.profile_code ""profileCode"", 
                                        pm.menu_code ""menuCode"",
                                        ml.menu_name ""menuName"",
                                        m.icon,
                                        pm.xmin ""rowVersion""
                                 from su.profile_menu pm
                                 inner join su.menu_label ml on pm.menu_code = ml.menu_code and lower(ml.language_code) = lower(@lang)
                                 inner join su.menu m on ml.menu_code = m.menu_code
                                 where pm.profile_code = @profileCode
                                 order by pm.menu_code");

                profile.ProfileMenus = (List<ProfileMenuDTO>)await _context.QueryAsync<ProfileMenuDTO>(sql.ToString(), new { profileCode = request.ProfileCode, lang = _user.Language }, cancellationToken);
            }

            return profile;
        }
    }
}
