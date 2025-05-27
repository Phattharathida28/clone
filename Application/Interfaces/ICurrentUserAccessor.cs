namespace Application.Interfaces;

public interface ICurrentUserAccessor
{
    long UserId { get; }
    string UserName { get; }
    string Company { get; }
    string Organization { get; }
    string Language { get; }
    string ProgramCode { get; }
}
