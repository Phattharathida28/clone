using Application.Common.Extensions;
using Application.Features.QORT01.DTO;
using Application.Interfaces;
using DocumentFormat.OpenXml.ExtendedProperties;
using Domain.Entities.DB;
using Domain.Entities.IN;
using Domain.Entities.IV;
using Domain.Entities.QO;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.QORT01.Query
{
    public class Detail
    {
        public class Query : IRequest<DetailDTO>
        {
            public int PLId { get; set; }
        }

        public class Handler : IRequestHandler<Query, DetailDTO>
        {
            private readonly ICleanDbContext _context;
            private readonly ICurrentUserAccessor _user;
            public Handler(ICleanDbContext context, ICurrentUserAccessor user)
            {
                _context = context;
                _user = user;
            }

            public async Task<DetailDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var master = await (from qo in _context.Set<QoPriceMaster>()
                                    join curr in _context.Set<DbCurrency>() on qo.CurrCode equals curr.CurrCode
                                    where qo.OuCode == "1001" && qo.PlId == request.PLId
                                    select new DetailDTO
                                    {
                                        PLId = qo.PlId,
                                        //PLTypeCode = qo.PlType == "W" ? PLType.W : PLType.R,
                                        PLTypeCode = qo.PlType,
                                        PLTypeName = qo.PlType == "W" ? PLType.W.GetDescription() : PLType.R.GetDescription(),
                                        PLCode = qo.PlCode,
                                        PLNameTH = qo.PlNameTha,
                                        PLNameEN = qo.PlNameEng,
                                        CurrencyCode = qo.CurrCode,
                                        CurrencyName = qo.CurrCode + " - " + curr.CurNameTha,
                                        //TaxTypeCode = qo.VatType == "E" ? TaxType.TaxExclude : TaxType.TaxInclude,
                                        TaxTypeCode = qo.VatType,
                                        TaxTypeName = qo.VatType == "E" ? TaxType.TaxExclude.GetDescription() : TaxType.TaxInclude.GetDescription(),
                                        Remark = qo.Remark,

                                    }).FirstOrDefaultAsync(cancellationToken);


                if (master == null) throw new Exception("ไม่พบแฟ้มราคา");


                var detailItems = await (from item in _context.Set<QoPriceDetail>()

                                         join goods in _context.Set<InGoods>() on item.ItemId equals goods.ItemId into goodsGroup
                                         from g in goodsGroup.DefaultIfEmpty()
                                         where g.OuCode == item.OuCode

                                         join unitPrice in _context.Set<DbUnit>() on item.UnitId equals unitPrice.UnitId

                                         where item.OuCode == "1001" && item.PlId == request.PLId
                                         select new DetailGoodsServiceDTO
                                         {
                                             PLDetId = item.PlDetId,
                                             EditTypeCode = (EditType)item.EditType,
                                             EditTypeName = ((EditType)item.EditType).GetDescription(),
                                             ItemId = item.ItemId,
                                             ItemName = $"{g.ItemCode} - {g.ItemName}",
                                             UnitId = item.UnitId,
                                             UnitCode = unitPrice.UnitCode,
                                             UnitName = unitPrice.UnitName,
                                             StandardPrice = item.PriceStandard,
                                             Price = item.Price,

                                         }).ToListAsync(cancellationToken);


                master.DetailItems = detailItems;
                return master;



            }
        }
    }
}
