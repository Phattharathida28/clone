using Application.Interfaces;
using MediatR;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Application.Features.SU.Localize;

public class List
{
    public class Localize
    {
        public Object Menu { get; set; }
        public Object Message { get; set; }
        public Object Label { get; set; }
    }

    public class Query : IRequest<Localize>
    {
        public string Lang { get; set; }

        public string Module { get; set; }
    }

    public class Handler : IRequestHandler<Query, Localize>
    {
        private readonly ICleanDbContext _context;
        public Handler(ICleanDbContext context) => _context = context;
        public async Task<Localize> Handle(Query request, CancellationToken cancellationToken)
        {
            JsonElement messageJson = JsonDocument.Parse("{}").RootElement;
            JsonElement menuJson = JsonDocument.Parse("{}").RootElement;

            if (request.Module == "all")
            {
                string messages = await _context.QuerySingleAsync<string>(@"SELECT json_object_agg(message_code ,message_desc)  as Json FROM su.message where lower(language_code) = @lang", new { lang = request.Lang.ToLower() }, token: cancellationToken);

                messageJson = JsonDocument.Parse(messages ?? "{}").RootElement;

                string menus = await _context.QuerySingleAsync<string>(@"SELECT json_object_agg(menu_code, menu_name) as Json FROM su.menu_label where lower(language_code) = @lang", new { lang = request.Lang }, token: cancellationToken);

                menuJson = JsonDocument.Parse(menus ?? "{}").RootElement;
            }

            string labels = await _context.QuerySingleAsync<string>(@"SELECT json_object_agg(concat(program_code ,'.',field_name) , label_name) as Json FROM su.program_label where lower(language_code) = @Lang and lower(module_code)= @module", new { lang = request.Lang, module = request.Module }, token: cancellationToken);
            
            JsonElement labelJson = JsonDocument.Parse(labels ?? "{}").RootElement;

            return new Localize() { Message = messageJson, Menu = menuJson, Label = labelJson };

        }
    }
}
