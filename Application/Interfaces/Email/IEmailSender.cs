using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Email;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message,bool runInBackground=true);
    Task SendEmailAsync(string email, string subject, string message,IEnumerable<string> attachmentPaths,bool runInBackground=true);
    Task SendEmailAsync(string email, string subject, string message, IEnumerable<Attachment> attachment,bool runInBackground=true);
    Task SendEmailWithTemplateAsysnc(string templateCode, string email, IReadOnlyDictionary<string, string> headerParam, IReadOnlyDictionary<string, string> bodyParam,bool runInBackground=true);
    Task SendEmailWithTemplateAsysnc(string templateCode, string email, IReadOnlyDictionary<string, string> headerParam, IReadOnlyDictionary<string, string> bodyParam, IEnumerable<string> attachmentPaths,bool runInBackground=true);
    Task SendEmailWithTemplateAsysnc(string templateCode, string email, IReadOnlyDictionary<string, string> headerParam, IReadOnlyDictionary<string, string> bodyParam, IEnumerable<Attachment> attachment,bool runInBackground=true);
}
