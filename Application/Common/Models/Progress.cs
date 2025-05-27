using System;

namespace Application.Common.Models;

public class Progress
{
    public decimal? Percent { get; set; }
    public string Message { get; set; }
    public bool? HaveLog { get; set; }

    public Progress()
    {

    }
    public Progress(decimal percent, string message, bool haveLog)
    {
        Percent = Math.Round((decimal)percent, 2);
        Message = message;
        HaveLog = haveLog;
    }
    public Progress(decimal percent,string message) : this(percent,message,false)
    {
         
    }
    public Progress(decimal percent) : this(percent, string.Empty, false)
    {

    }
}
