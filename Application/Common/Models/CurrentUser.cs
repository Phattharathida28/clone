namespace Application.Common.Models;

public class CurrentUser
{
    public long? UserId { get; set; }
    public string CompanyCode { get; set; }
    public string UserName { get; set; }
    public string ProgramCode { get; set; }
    public string Language { get; set; }
}
