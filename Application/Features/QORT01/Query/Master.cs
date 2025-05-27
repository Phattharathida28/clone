using Application.Common.Extensions;
using Application.Common.Models;
using Application.Interfaces;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities.DB;
using Domain.Entities.IN;
using Domain.Entities.QO;
using Domain.Entities.SU;
using Domain.Enums;
using iText.Layout.Renderer;
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
    public class Master
    {
        public class MasterData
        {
            public IEnumerable<dynamic> DDLCurrency { get; set; } //สลุลเงิน
            public IEnumerable<dynamic> DDLEditType { get; set; } //การแก้ไข
            public IEnumerable<dynamic> DDLPLType { get; set; } //แฟ้มราคา
            public IEnumerable<dynamic> DDLUnit { get; set; } //หน่วย
            public IEnumerable<dynamic> DDLPLCode { get; set; } //แฟ้มราคา
            public IEnumerable<dynamic> DDLGoodsService { get; set; } //สินค้าและบริการ
            public IEnumerable<dynamic> DDLTaxType { get; set; } //ประเภทภาษี


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
                MasterData masterData = new MasterData();
                masterData.DDLCurrency = await GetCurrency(cancellationToken);
                masterData.DDLEditType = await GetEditType(cancellationToken);
                masterData.DDLPLType = await GetPLType(cancellationToken);
                masterData.DDLUnit = await GetUnit(cancellationToken);

                masterData.DDLPLCode = await GetPLCode(cancellationToken);
                masterData.DDLGoodsService = await GetGoodsService(cancellationToken);
                masterData.DDLTaxType = await GetTaxType(cancellationToken);


                return masterData;
            }

            private async Task<List<DropDownDto>> GetTaxType(CancellationToken cancellationToken)
            {
                var data = new List<DropDownDto>();
                var programLabelList = await _context.Set<ProgramLabel>().Where(label => label.ProgramCode == "QORT01"
                                                                                        && _user.Language.ToLower() == label.LanguageCode.ToLower()
                                                                                        && (label.FieldName.ToLower() == ("TotalVAT").ToLower() || label.FieldName.ToLower() == ("WithOutVAT").ToLower())
                                                                                        ).ToListAsync();
                if (programLabelList != null && programLabelList.Count > 0)
                {
                    programLabelList.ForEach(label =>
                    {
                        if (string.Equals(label.FieldName, "TotalVAT", StringComparison.OrdinalIgnoreCase) || string.Equals(label.FieldName, "WithOutVAT", StringComparison.OrdinalIgnoreCase))
                        {
                            data.Add(new DropDownDto()
                            {
                                Value = label.FieldName.ToLower() == ("WithOutVAT").ToLower() ?
                                                                "E" : "I",
                                Label = label.LabelName
                            });
                        }
                    });
                }
                return data;
            }

            private async Task<IEnumerable<dynamic>> GetCurrency(CancellationToken cancellationToken)
            {

                var result = await (from currency in _context.Set<DbCurrency>()
                                    where currency.Active == "Y" && currency.OuCode == "1001"
                                    orderby currency.CurNameTha
                                    select new DropDownDto
                                    {
                                        Value = currency.CurrCode,
                                        Text = currency.CurrCode + "-" + currency.CurNameTha,
                                        Label = string.Concat(currency.CurrCode, "-", string.Equals(_user.Language, "TH", StringComparison.OrdinalIgnoreCase) ? currency.CurNameTha : currency.CurNameEng)
                                    }
                                    ).ToListAsync(cancellationToken);

                return result;


            }


            private async Task<IEnumerable<dynamic>> GetEditType(CancellationToken cancellationToken)
            {
                var result = (from editType in Enum.GetValues(typeof(EditType)).Cast<EditType>()
                              orderby editType
                              select new DropDownDto
                              {
                                  Value = (int)editType == 1 ? "Unchanged" : (int)editType == 2 ? "PriceChange" : "Added",
                                  Value2 = (int)editType,
                                  Text = editType.GetDescription(),
                                  Label = editType.GetDescription()
                              }).ToList();

                return result;

            }

            private async Task<IEnumerable<dynamic>> GetUnit(CancellationToken cancellationToken)
            {

                var result = await (from unit in _context.Set<DbUnit>()
                                    where unit.Active == "Y" && unit.OuCode == "1001"
                                    orderby unit.UnitName
                                    select new DropDownDto
                                    {
                                        Value = unit.UnitId.ToString(),
                                        Text = unit.UnitCode + "-" + unit.UnitCode,
                                        Label = string.Concat(unit.UnitCode, "-", string.Equals(_user.Language, "TH", StringComparison.OrdinalIgnoreCase) ? unit.UnitName : unit.UnitNameEng)
                                    }
                                    ).ToListAsync(cancellationToken);

                return result;


            }


            private async Task<IEnumerable<dynamic>> GetPLType(CancellationToken cancellationToken)
            {

                var result = (from plType in Enum.GetValues(typeof(PLType)).Cast<PLType>()
                              orderby plType
                              select new DropDownDto
                              {
                                  Value = (int)plType == 1 ? "W" : "R",
                                  //Value2 = (int)plType,
                                  Text = plType.GetDescription(),
                                  Label = plType.GetDescription()
                              }).ToList();

                return result;
            }

            private async Task<IEnumerable<dynamic>> GetPLCode(CancellationToken cancellationToken)
            {

                var result = await (from qoMaster in _context.Set<QoPriceMaster>()
                                    where qoMaster.OuCode == "1001"
                                    orderby qoMaster.PlNameTha
                                    select new DropDownDto
                                    {
                                        //Value = qoMaster.PlCode,
                                        Value = qoMaster.PlId.ToString(),
                                        Text = qoMaster.PlCode + " - " + qoMaster.PlNameTha,
                                        Label = string.Concat(qoMaster.PlCode, " - ", string.Equals(_user.Language, "TH", StringComparison.OrdinalIgnoreCase) ? qoMaster.PlNameTha : qoMaster.PlNameEng),
                                    }
                                    ).ToListAsync(cancellationToken);

                return result;

            }


            private async Task<IEnumerable<dynamic>> GetGoodsService(CancellationToken cancellationToken)
            {
                //var result1 = await (from service in _context.Set<IvServiceItem>()
                //                     where service.OuCode == "1001"
                //                     orderby service.ItemId
                //                     select new DropDownDto
                //                     {
                //                         //Value = service.ItemCode,
                //                         Value = service.ItemId.ToString(),
                //                         Text = service.ItemCode + " - " + service.ItemName,
                //                         Text2 = ProductType.S.GetDescription(),
                //                         Label = string.Concat(service.ItemCode , " - " , string.Equals(_user.Language, "TH",StringComparison.OrdinalIgnoreCase ) ? service.ItemName : service.ItemNameEng)
                //                     }).ToListAsync(cancellationToken);


                var result2 = await (from goods in _context.Set<InGoods>()
                                     where goods.OuCode == "1001"
                                     orderby goods.ItemId
                                     select new DropDownDto
                                     {
                                         //Value = goods.ItemCode,
                                         Value = goods.ItemId.ToString(),
                                         Text = goods.ItemCode + " - " + goods.ItemName,
                                         Text2 = ProductType.F.GetDescription(),
                                         Label = string.Concat(goods.ItemCode, " - ", string.Equals(_user.Language, "TH", StringComparison.OrdinalIgnoreCase) ? goods.ItemName : goods.ItemNameEng),
                                     }).ToListAsync(cancellationToken);



                var combineResult = result2;

                return combineResult;
            }






        }

    }
}

