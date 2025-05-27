using System;

namespace Domain.Entities;

public enum RowState
{
    Normal, Add, Edit, Delete
}


public interface EntityDomain
{
    string CreatedBy { get; }
    DateTime? CreatedDate { get; }
    string CreatedProgram { get; }
    string UpdatedBy { get; }
    DateTime? UpdatedDate { get; }
    string UpdatedProgram { get; }
    void CreateAudit(string user, string prog);
    void UpdateAudit(string user, string prog);
}
public abstract class EntityBase : EntityDomain
{
    public string Guid { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public string CreatedProgram { get; private set; }
    public string UpdatedBy { get; private set; }
    public DateTime? UpdatedDate { get; private set; }
    public string UpdatedProgram { get; private set; }
    public uint? RowVersion { get; set; }
    public RowState? RowState { get; set; }
    public EntityBase()
    {
        this.Guid = System.Guid.NewGuid().ToString();
        this.RowState = Entities.RowState.Normal;
    }

    public void CreateAudit(string user, string prog)
    {
        CreatedBy = user;
        CreatedDate = DateTime.Now;
        CreatedProgram = prog;
    }

    public void UpdateAudit(string user, string prog)
    {
        UpdatedBy = user;
        UpdatedDate = DateTime.Now;
        UpdatedProgram = prog;
    }
}
