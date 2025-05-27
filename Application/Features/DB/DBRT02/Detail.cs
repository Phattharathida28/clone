using Application.Interfaces;
using Domain.Entities.DB;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.DB.DBRT02;

public class Detail
{
    public class Query : IRequest<ListItemGroup>
    {
        public string ListItemGroupCode { get; set; }
    }

    public class Handler : IRequestHandler<Query, ListItemGroup>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;

        public async Task<ListItemGroup> Handle(Query request, CancellationToken cancellationToken)
        {
            StringBuilder sql = new StringBuilder();
            ListItemGroup listItemGroup = new();

            sql.AppendLine(@"
            select 
	            lig.list_item_group_code ""listItemGroupCode"",
	            lig.list_item_group_name ""listItemGroupName"",
	            lig.xmin ""rowVersion""
            from db.list_item_group lig 
            where lig.list_item_group_code = @ListItemGroupCode");

            listItemGroup = await _context.QueryFirstOrDefaultAsync<ListItemGroup>(sql.ToString(), new { request.ListItemGroupCode }, cancellationToken) ?? new();

            sql = new();
            sql.AppendLine(@"
            select 
	            ligl.list_item_code ""listItemCode"",
	            ligl.list_item_group_code ""listItemGroupCode"",
	            ligl.list_item_name_tha ""listItemNameTha"",
	            ligl.list_item_name_eng ""listItemNameEng"",
	            ligl.xmin ""rowVersion""
            from db.list_item ligl 
            where ligl.list_item_group_code = @ListItemGroupCode");

            listItemGroup.ListItems = (List<ListItem>)await _context.QueryAsync<ListItem>(sql.ToString(), new { request.ListItemGroupCode }, cancellationToken);

            return listItemGroup;
        }
    }
}
