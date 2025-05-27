using Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Exceptions;

public class ErrorMessage
{
    public string Code { get; private set; }
    public IEnumerable<string> Parameters { get; private set; }
    public bool? TranslateFlag { get; private set; }
    public string TranslatedMessage { get; private set; }
    public ErrorMessage(string messageCode, bool translate = false)
    {
        this.Code = messageCode;
        this.TranslateFlag = translate;
    }
    public ErrorMessage(string messageCode, params string[] parameters)
    {
        this.Code = messageCode;
        this.Parameters = parameters;
    }

    public ErrorMessage(bool translate, string messageCode, params string[] parameters)
    {
        this.Code = messageCode;
        this.Parameters = parameters;
        this.TranslateFlag = translate;
    }

    private async Task Translate(ICleanDbContext context, string lang)
    {
        this.TranslatedMessage = Code;

        string[] code = this.Code.Split('.');

        if (code.Length == 2)
        {
            string messageDesc = await context.ExecuteScalarAsync<string>("select message_desc from su.message  where message_code = @Code and language_code = @Lang", new { Code = code[1], Lang = lang }, default);
            
            if (messageDesc != null)
            {
                messageDesc = messageDesc.Replace("{{", "{").Replace("}}", "}");
                this.TranslatedMessage = messageDesc;

                if (this.Parameters != null)
                {
                    List<string> paramDescriptions = new();

                    foreach (string parameter in this.Parameters)
                    {
                        string[] paramCode = parameter.Split('.');
                        string paramDesc = string.Empty;

                        if (paramCode.Length >= 2)
                        {
                            switch (paramCode[0])
                            {
                                case "label":
                                    paramDesc = await context.ExecuteScalarAsync<string>("select label_name from su.program_label where program_code =  @ProgramCode and field_name = @Name and language_code = @Lang ", new { ProgramCode = paramCode[1], Name = paramCode[2], Lang = lang }, default);
                                    break;
                                case "message":
                                    paramDesc = await context.ExecuteScalarAsync<string>("select message_desc from su.message  where message_code = @Code and language_code = @Lang", new { Code = paramCode[1], Lang = lang }, default);
                                    break;
                            }
                        }

                        paramDescriptions.Add(paramDesc);
                    }

                    this.TranslatedMessage = string.Format(messageDesc, paramDescriptions.ToArray());
                }
            }
        }
    }

    public async Task<ErrorMessage> GetMessage(ICleanDbContext context, string lang)
    {
        if (this.TranslateFlag == true) await this.Translate(context, lang);

        return this;
    }
}
