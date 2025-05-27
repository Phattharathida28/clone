namespace Application.Common.Models;

public abstract class RequestPageQuery
{
    public int Page { get; set; }
    private int _size;
    public int Size
    {
        get
        {
            return _size == 0 ? 10 : _size;
        }
        set
        {
            _size = value;
        }
    }
    public string Sort { get; set; }
}
