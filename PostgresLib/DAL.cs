using Npgsql;
using PostgresLib.Types;
using System.Configuration;
using System.Data;
//using CommonLib;

namespace PostgresLib;

public static class DAL
{
    private static readonly string connString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

    public static int CreateFromDictionary(string name, IDictionary<string, DbTypes> columns)
    {
        if (columns is null) throw new ArgumentNullException(nameof(columns));
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

        string headers = $"({string.Join(",", columns.Select(c => c.Key + " " + c.Value).ToArray())})";

        return ExecuteAnyAsync($"create table {name} {headers}").Result;
    }

    public async static Task<int> ExecuteAnyAsync(string sql)
    {
        using NpgsqlConnection conn = new(connString);
        conn.Open();

        using NpgsqlCommand cmd = new(sql, conn);
        return await cmd.ExecuteNonQueryAsync();
    }

    //public async static Task<int> InsertLog(string message, LogLevel level = LogLevel.Info)
    //{
    //    string sql = $"insert into logs (message,level) values (\'{message}\',\'{Enum.GetName(typeof(LogLevel), level)}\')";
    //    return await ExecuteAnyAsync(sql);
    //}
}