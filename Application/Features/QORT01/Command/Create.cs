using Application.Features.QORT01.DTO;
using Application.Interfaces;
using Domain.Entities.DB;
using Domain.Entities.QO;
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
    public class Create
    {
        public class Command : IRequest<int>
        {
            public string PLTypeCode { get; set; }
            public string PLCode { get; set; }
            public string PLNameTH { get; set; }
            public string PLNameEN { get; set; }
            public string CurrencyCode { get; set; }
            public string? TaxTypeCode { get; set; }
            public string? Remark { get; set; }
            public bool? IsAuto { get; set; }

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

                var duplicateName = _context.Set<QoPriceMaster>().Where(x => (request.IsAuto == false && x.PlCode == request.PLCode) && x.PlNameTha == request.PLNameTH).FirstOrDefault();
                if (duplicateName != null) throw new Exception("ชื่อแฟ้มราคาซ้ำ");

                var master = new QoPriceMaster
                {
                    OuCode = "1001",
                    PlCode = request.IsAuto == true ? await GenerateUniqueCodeAsync(request.PLTypeCode) : request.PLCode,
                    PlType = request.PLTypeCode,
                    PlNameTha = request.PLNameTH,
                    PlNameEng = request.PLNameEN,
                    CurrCode = request.CurrencyCode,
                    VatType = request.TaxTypeCode,
                    Remark = request.Remark,
                };

                await _context.Set<QoPriceMaster>().AddAsync(master, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken); //insert QO master เพื่อให้ได้ PLId



                var detailList = request.DetailItems.Select(x => new QoPriceDetail
                {
                    PlId = master.PlId,
                    OuCode = "1001",//fix
                    ItemId = x.ItemId,
                    UnitId = x.UnitId,
                    ProductType = "F",//fix
                    Price = x.Price,
                    EditType = x.EditType.ToLower() == ("Unchanged").ToLower() ? 1 : 2,

                }).ToList();

                await _context.Set<QoPriceDetail>().AddRangeAsync(detailList, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken); //insert QO detail

                return master.PlId;

            }

            private async Task<string> GenerateUniqueCodeAsync(string input)
            {
                string prefix;

                switch (input)
                {
                    case "W":
                        prefix = "STD";
                        break;
                    case "R":
                        prefix = "PLR";
                        break;
                    default:
                        throw new ArgumentException("Input ต้องเป็น W หรือ R เท่านั้น");
                }

                int running = 1;
                string newCode;

                do
                {
                    newCode = $"{prefix}{running.ToString("D4")}";

                    bool exists = await _context.Set<QoPriceMaster>()
                        .AnyAsync(x => x.PlCode == newCode);

                    if (!exists)
                        break;

                    running++;

                } while (true);

                return newCode;
            }
        }
    }
}
