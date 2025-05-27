using Application.Features.QORT01.DTO;
using Application.Interfaces;
using Domain.Entities.DB;
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

namespace Application.Features.QORT01.Command
{
    public class Update
    {
        public class Command : IRequest<int>
        {
            public int PLId { get; set; }
            public string PLTypeCode { get; set; }
            public string PLCode { get; set; }
            public string PLNameTH { get; set; }
            public string PLNameEN { get; set; }
            public string CurrencyCode { get; set; }
            public string? TaxTypeCode { get; set; }
            public string? Remark { get; set; }

            public List<QoPriceDetailDTO> DetailItems { get; set; } = new();
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly ICleanDbContext _context;
            public Handler(ICleanDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var master = _context.Set<QoPriceMaster>()
                      .FirstOrDefault(x => x.PlId == request.PLId);

                if (master == null)
                    throw new Exception("ไม่พบข้อมูลแฟ้มราคา");

                //อัพเดตค่าแฟ้ม
                master.PlNameTha = request.PLNameTH;
                master.PlNameEng = request.PLNameEN;
                master.CurrCode = request.CurrencyCode;
                master.Remark = request.Remark;
                master.VatType = request.TaxTypeCode;


                //อัพเดตค่าใน detail

                //1.ดึง detail เก่า
                var existingDetails = await _context.Set<QoPriceDetail>()
                    .Where(x => x.PlId == request.PLId)
                    .ToListAsync(cancellationToken);

                // loop รายการใหม่จาก request
                foreach (var item in request.DetailItems)
                {
                    var exitting = existingDetails.FirstOrDefault(x => x.ItemId == item.ItemId && x.UnitId == item.UnitId);
                    if (exitting != null)
                    {
                        //update
                        exitting.PriceStandard = item.Price;
                        exitting.Price = item.Price;
                        exitting.EditType = item.EditType.ToLower() == ("Unchanged").ToLower() ? 1 : 2;
                    }
                    else
                    {
                        //add
                        var newDetail = new QoPriceDetail
                        {
                            PlId = master.PlId,
                            OuCode = "1001",//fix
                            ItemId = item.ItemId,
                            UnitId = item.UnitId,
                            ProductType = "F",//fix
                            Price = item.Price,
                            PriceStandard = item.Price,
                            EditType = item.EditType.ToLower() == ("Unchanged").ToLower() ? 1 : 2,
                        };

                        await _context.Set<QoPriceDetail>().AddAsync(newDetail, cancellationToken);
                    }

                    await _context.SaveChangesAsync(cancellationToken); //insert QO detail
                }



                return master.PlId;





            }


        }
    }
}
