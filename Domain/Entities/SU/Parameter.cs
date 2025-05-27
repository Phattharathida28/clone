namespace Domain.Entities.SU;

public class Parameter : EntityBase 
{
    public string ParameterGroupCode { get; set; }  
    public string ParameterCode{ get; set;}
    public string ParameterValue { get; set; }
    public string Remark { get; set; }
}
