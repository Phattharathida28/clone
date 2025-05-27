using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Common.Extensions;

public static class StringAppend
{
    public static void AppendOR(this StringBuilder sb,params string[] statements)
    {
        Regex rgx = new Regex("^and");
        statements = statements.Where(s=>s != null).Select(s => {
           s = rgx.Replace(s.Trim(),string.Empty, 1);
           return $"({s})";
        }).ToArray();
        sb.AppendLine("and (");
        sb.AppendLine(string.Join(" OR ",statements));
        sb.AppendLine(")");
    }

}
