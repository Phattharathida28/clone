using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IHttpService
{
    Task<T> GetAsync<T>(string requestUrl);
    Task<byte[]> GetAsByteArrayAsync(string requestUrl);
    Task<T> PostAsync<T>(string requestUrl, object bodyContent);
    Task<T> PostAsync<T>(string requestUrl, HttpContent content);
    Task<byte[]> PostAsByteArrayAsync(string requestUrl, object bodyContent);
    Task<dynamic> PostAsAsync(string requestUrl, object bodyContent);
    Task<string> GetAsyncReturnString(string requestUrl);
    Task PostAsync(string requestUrl, object bodyContent,string tokenType,string accessToken,string propertyKey=null);
    Task<HttpResponseMessage> PostAsAsync(string requestUrl, object bodyContent,string tokenType,string accessToken, string propertyKey = null);
}
