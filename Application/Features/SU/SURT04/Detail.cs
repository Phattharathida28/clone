using Application.Interfaces;
using Domain.Entities.SU;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.SU.SURT04;

public class Detail
{
    public class Query : IRequest<UserDTO>
    {
        public int UserId { get; set; }
    }

    public class UserDTO : User
    {
        public List<UserProfile> UserProfiles { get; set; }
    }

    public class Handler : IRequestHandler<Query, UserDTO>
    {
        private readonly ICleanDbContext _context;
        private readonly ICurrentUserAccessor _user;
        public Handler(ICleanDbContext context, ICurrentUserAccessor user)
        {
            _context = context;
            _user = user;
        }
        public async Task<UserDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            UserDTO user = new UserDTO();

            sql.AppendLine(@"select 
	                                user_id ""Id"", 
	                                user_name ""userName"", 
	                                password_hash ""passwordHash"", 
	                                email, 
	                                phone_number ""phoneNumber"", 
	                                firstname, 
	                                lastname,
                                    firstname_th ""firstnameTh"", 
	                                lastname_th ""lastnameTh"", 
	                                active,
	                                xmin ""rowState""
                                from su.user
                                where user_id = @userId");

            user = await _context.QueryFirstOrDefaultAsync<UserDTO>(sql.ToString(), new { userId = request.UserId }, cancellationToken);

            if (user != null)
            {
                sql = new StringBuilder();
                sql.AppendLine(@"select 
	                                user_id ""userId"", profile_code ""profileCode"", xmin ""rowVersion""
                                 from su.user_profile
                                 where user_id = @userId");

                user.UserProfiles = (List<UserProfile>)await _context.QueryAsync<UserProfile>(sql.ToString(), new { userId = request.UserId, lang = _user.Language }, cancellationToken);
            }

            return user;
        }
    }
}
