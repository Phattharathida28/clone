using Application.Common.Extensions;
using Application.Common.Models;
using Application.Features.QORT01.DTO;
using Application.Interfaces;
using Domain.Entities.DB;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Application.Features.QORT01.Query
{
    public class List
    {
        public class Query : RequestPageQuery, IRequest<PageDto>
        {
            public string? Keyword { get; set; } //รหัสแฟ้ม , ชื่อแฟ้ม
            public string? PLTypeCode { get; set; } //รหัส ประเภทแฟ้มราาคา ชุดราคา
            public string? CurrencyCode { get; set; } ///รหัส สกุลเงิน
            public string? TaxTypeCode { get; set; } //รหัส ประเภทภาษี
            public int? PLId { get; set; } //รหัส แฟ้ม
        }

        public class Handler : IRequestHandler<Query, PageDto>
        {
            private readonly ICleanDbContext _context;
            private readonly ICurrentUserAccessor _user;

            public Handler(ICleanDbContext context, ICurrentUserAccessor user)
            {
                _context = context;
                _user = user;
            }

            public async Task<PageDto> Handle(Query request, CancellationToken cancellationToken)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(@"
                    select qpm.pl_id ""plId""
                    , case when lower(qpm.pl_type) = 'W' then @PLTypeW else @PLTypeR end ""plTypeCode""
                    , case when lower(qpm.pl_type) = lower('W') then @PLTypeWDesc else @PLTypeRDesc end ""plTypeName""
                    , qpm.pl_code ""plCode""
                    , case when @Lang = 'th' then pl_name_tha  else pl_name_eng  end ""plName""
                    , qpm.curr_code ""currencyCode""
                    , case when @Lang='th' then concat(dc.curr_code ,' - ',dc.cur_name_tha) else concat(dc.curr_code ,' - ',dc.cur_name_eng) end ""currencyName""
                    , case when lower(qpm.vat_type) =lower('E') then @TaxExclude  else @TaxInclude end ""taxTypeCode""
                    , case when lower(qpm.vat_type) =lower('E') then @TaxExcludeDesc else @TaxIncludeDesc end ""taxTypeName""
                    from ""Back-End-Team"".qo_price_master qpm  
                    left join ""Back-End-Team"".db_currency dc on qpm.curr_code  = dc.curr_code
                                                               and qpm.ou_code = dc.ou_code
                    where 1=1
                    and qpm.ou_code = '1001'
                ");

                if (!string.IsNullOrWhiteSpace(request.Keyword))
                {
                    sql.AppendLine("and qpm.pl_code ilike concat('%', @Keywords ,'%') or qpm.pl_name_tha ilike concat('%', @Keywords ,'%') or qpm.pl_name_eng ilike concat('%', @Keywords ,'%')");
                }


                if (request.PLTypeCode != null)
                {
                    sql.AppendLine("and qpm.pl_type = @PLTypeCode");
                }

                if (!string.IsNullOrEmpty(request.CurrencyCode))
                {
                    sql.AppendLine("and qpm.curr_code = @CurrencyCode");
                }

                if (request.TaxTypeCode != null)
                {
                    sql.AppendLine("and qpm.vat_type = @TaxTypeCode");
                }

                if (request.PLId != null)
                {
                    sql.AppendLine("and qpm.pl_id = @PLCode");
                }



                var res = await _context.GetPage(sql.ToString(),
                                                 new
                                                 {
                                                     Keywords = request.Keyword,
                                                     PLTypeCode = request.PLTypeCode,
                                                     CurrencyCode = request.CurrencyCode,
                                                     PLCode = request.PLId,
                                                     TaxTypeCode = request.TaxTypeCode,
                                                     Lang = _user.Language,
                                                     PLTypeW = PLType.W,
                                                     PLTypeR = PLType.R,
                                                     PLTypeWDesc = PLType.W.GetDescription(),
                                                     PLTypeRDesc = PLType.R.GetDescription(),
                                                     TaxExclude = TaxType.TaxExclude,
                                                     TaxInclude = TaxType.TaxInclude,
                                                     TaxExcludeDesc = TaxType.TaxExclude.GetDescription(),
                                                     TaxIncludeDesc = TaxType.TaxInclude.GetDescription(),
                                                 }, request, cancellationToken);
                return res;

            }
        }
    }
}
