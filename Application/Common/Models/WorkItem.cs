using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Models;

public class PercentUpdatedEventArgs : EventArgs
{
    public decimal Percent { get; set; }
    public object Info { get; set; }
}
public class WorkItem
{
    public string Id { get; private set; }
    public Func<CancellationToken, IServiceProvider,Action<decimal,object>, ValueTask> Action { get; private set; }
    public decimal PercentComplele {  get; private set; }

    public event EventHandler<PercentUpdatedEventArgs> PercentUpdated;
    public WorkItem(string id, Func<CancellationToken, IServiceProvider, Action<decimal, object>, ValueTask> action)
    {
        Id = id;
        Action = action;
    }

    public void UpdateProgress(decimal percent,object info)
    {
        this.PercentComplele = Decimal.Round(percent,2);
        PercentUpdatedEventArgs args = new PercentUpdatedEventArgs();
        args.Percent = this.PercentComplele;
        args.Info = info;
        OnPercentUpdated(args);
    }


    protected virtual void OnPercentUpdated(PercentUpdatedEventArgs e)
    {
        EventHandler<PercentUpdatedEventArgs> handler = PercentUpdated;

        if (handler != null) handler(this, e);
    }
}
