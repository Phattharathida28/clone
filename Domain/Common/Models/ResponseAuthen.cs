namespace Domain.Common.Models;

public class ResponseAuthen
{
    public string Status { get; set; }
    public string StatusContent { get; set; }
    public DataDetail Data { get; set; }
}
public class DataDetail
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string EnpiresIn { get; set; }
    public string TokenType { get; set; }
}
