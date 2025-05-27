namespace Domain.Entities.SU;

public class ApiClient : EntityBase
{
    public int Id { get; set; }
    public string ClientId { get; set; }
    public string ClientName { get; set; }
    public string PublicKey { get; set; }
}