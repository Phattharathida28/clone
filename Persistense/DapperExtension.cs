using Application.Common.Models;
using Application.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Persistense;

public partial class CleanDbContext : DbContext, ICleanDbContext
{
    private IOrderedQueryable<T> OrderByMember<T>(IQueryable<T> source, string memberPath, bool descending)
    {
        var parameter = Expression.Parameter(typeof(T), "item");
        var member = memberPath.Split('.')
            .Aggregate((Expression)parameter, Expression.PropertyOrField);
        var keySelector = Expression.Lambda(member, parameter);
        var methodCall = Expression.Call(
            typeof(Queryable), descending ? "OrderByDescending" : "OrderBy",
            new[] { parameter.Type, member.Type },
            source.Expression, Expression.Quote(keySelector));
        return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
    }

    public Task<int> ExecuteAsync(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().ExecuteAsync(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().ExecuteScalarAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> QueryFirstAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QueryFirstAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QueryFirstOrDefaultAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> QuerySingleAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QuerySingleAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QuerySingleOrDefaultAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QueryAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> GetParameterValue<T>(string group, string code, CancellationToken token) => this.Database.GetDbConnection().ExecuteScalarAsync<T>(new CommandDefinition("SELECT parameter_value FROM su_parameter WHERE parameter_group_code = @Group AND parameter_code = COALESCE(@Code,parameter_code)", new { Group = group, Code = code }, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: CommandType.Text));

    public Task<IEnumerable<(string, T)>> GetParameterValues<T>(string group, CancellationToken token = default) => this.Database.GetDbConnection().QueryAsync<(string, T)>(new CommandDefinition("SELECT parameter_code,parameter_value FROM su.parameter WITH(NOLOCK) WHERE parameter_group_code = @Group", new { Group = group }, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: CommandType.Text));

    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id", object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QueryAsync<TFirst, TSecond, TReturn>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text), map, splitOn);

    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id", object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QueryAsync<TFirst, TSecond, TThird, TReturn>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text), map, splitOn);

    public async Task<PageDto> GetPage(string sql, object param, RequestPageQuery page, CancellationToken token = default(CancellationToken))
    {
        string sort = string.Empty;

        if (page.Sort != null)
        {
            string[] sortPart = page.Sort.Split(",");
            string sortSQL = null;

            foreach (string column in sortPart)
            {
                string[] columnPart = column.Split(" ");
                sortSQL = string.Join(",", sortSQL, $"\"{columnPart[0]}\" {(columnPart.Length == 2 ? columnPart[1] : string.Empty)} ");
            }

            sort = $"ORDER BY {sortSQL.Substring(1, sortSQL.Length - 1)} ";
        }


        string query = $@"WITH page as
                          ({sql})
                           SELECT *
                           FROM(
                              SELECT page.*
                              FROM page 
                              LIMIT 1
                          ) AS for_total 
                           CROSS JOIN (SELECT Count(1) AS Count FROM page) AS {nameof(PageDto.Count)} 
                           UNION ALL
                           SELECT *
                           FROM( 
                           SELECT page.*,0 AS {nameof(PageDto.Count)}
                           FROM page
                           {sort} OFFSET {page.Page * page.Size} ROWS FETCH NEXT {page.Size} ROWS ONLY
                        ) AS for_data";


        List<dynamic> result = (List<dynamic>)await this.Database.GetDbConnection().QueryAsync<dynamic>(new CommandDefinition(query, param, cancellationToken: token));
        dynamic firstRow = result.FirstOrDefault();
        long count = 0;

        if (firstRow != null && (firstRow as IDictionary<string, object>)["count"] != null)
        {
            long.TryParse((firstRow as IDictionary<string, object>)["count"] + "", out count);
        };

        return new PageDto
        {
            Rows = result.Count() > 0 ? result.Skip(1) : result,
            Count = count
        };

    }
}
