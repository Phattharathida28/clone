using Application.Common.Constants;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Persistense;

public class NoLockInterceptor : DbCommandInterceptor
{
    private static Regex TableAliasRegex = new Regex(@"(?<tableAlias>((FROM)|(JOIN))\s\[([^\s]+)\]\sAS\s\[([^\s]+)\](?!\sWITH\s\(NOLOCK\)))", RegexOptions.Multiline | RegexOptions.IgnoreCase);

    public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
    {
        ManipulateCommand(command);

        return result;
    }

    public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
    {
        ManipulateCommand(command);

        return new ValueTask<InterceptionResult<object>>(result);
    }

    public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
    {
        ManipulateCommand(command);

        return result;
    }

    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
    {
        ManipulateCommand(command);

        return new ValueTask<InterceptionResult<DbDataReader>>(result);
    }

    private static void ManipulateCommand(DbCommand command)
    {
        if (command.CommandText.StartsWith($"-- {Hint.Nolock}", StringComparison.Ordinal)) command.CommandText = TableAliasRegex.Replace(command.CommandText, "${tableAlias} WITH (NOLOCK)");
    }
}
