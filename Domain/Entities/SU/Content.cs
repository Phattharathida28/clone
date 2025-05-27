namespace Domain.Entities.SU;

public class Content : EntityBase
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public bool? Reference { get; set; }
    public bool ValidatePath { get; set; }
    public string Container { get; set; }
    public int Size { get; set; }

    public Content()
    {
        ValidatePath = true;
    }
}
