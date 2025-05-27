using Application.Interfaces;
using Domain.Entities.DB;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.DB.DBRT02;

public class List
{
    public class Query : IRequest<List<ListItemGroup>>
    {
        public string Keywords { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<ListItemGroup>>
    {
        private readonly ICleanDbContext _context;

        public Handler(ICleanDbContext context, ICurrentUserAccessor user) => _context = context;

        public async Task<List<ListItemGroup>> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"
            select 
	            lig.list_item_group_code ""listItemGroupCode"",
	            lig.list_item_group_name ""listItemGroupName""
            from db.list_item_group lig
            where concat(lig.list_item_group_code, lig.list_item_group_name) ilike concat('%', @Keywords, '%')  ");

            return await _context.QueryAsync<ListItemGroup>(sql.ToString(), new { request.Keywords }, cancellationToken) as List<ListItemGroup>;
        }
    }
}
