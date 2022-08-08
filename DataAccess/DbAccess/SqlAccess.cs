using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccess.DbAccess;

public class SqlAccess : ISqlAccess
{
    private readonly IConfiguration _config;

    public SqlAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadSqlData<T, U>(string sqlProcedure, U parameters, string connectionId = "default")
    {
        using IDbConnection connection = new MySqlConnection(_config.GetConnectionString(connectionId));

        var rows = await connection.QueryAsync<T>(sqlProcedure, parameters);
        return rows;
    }

    public async Task WriteSqlData<T>(string sqlProcedure, T parameters, string connectionId = "default")
    {
        using IDbConnection connection = new MySqlConnection(_config.GetConnectionString(connectionId));

        await connection.ExecuteAsync(sqlProcedure, parameters);
    }

    public async Task<IEnumerable<T1>> LoadMultiMapSqlData<T1, T2, U>(string sqlProcedure, U parameters, Func<T1,T2,T1> func, string splitCol, string connectionId = "default")
    {
        using IDbConnection connection = new MySqlConnection(_config.GetConnectionString(connectionId)); 

        var rows = await connection.QueryAsync(sqlProcedure, func, parameters, splitOn: splitCol);
        return rows;
    }

    public async Task<IEnumerable<T1>> LoadMultiMapSqlData<T1, T2, T3, U>(string sqlProcedure, U parameters, Func<T1, T2, T3, T1> func, string splitCol, string connectionId = "default")
    {
        using IDbConnection connection = new MySqlConnection(_config.GetConnectionString(connectionId));

        var rows = await connection.QueryAsync(sqlProcedure, func, parameters, splitOn: splitCol);
        return rows;
    }

    // Task specific methods
    public async Task<FullCustomersModel> LoadCustomerInfo(string sql, object param, string split, string connId = "default")
    {
        using IDbConnection conn = new MySqlConnection(_config.GetConnectionString(connId));

        FullCustomersModel? customer = null;
        IEnumerable<OrdersModel>? orders = null;

        using (var lists = await conn.QueryMultipleAsync(sql, param))
        {
            customer = lists.Read<FullCustomersModel, PriceGroupsModel, FullCustomersModel>(
                (customer, pGroup) =>
                {
                    customer.PriceGroup = pGroup;
                    return customer;
                }, splitOn: split).Single();
            orders = lists.Read<OrdersModel>();
        }

        customer.Orders = orders;
        return customer;
    }

    public async Task<List<FullOrdersModel>> LoadManyOrderDetails(string sql, object param, string split, string connId = "default")
    {
        using IDbConnection conn = new MySqlConnection(_config.GetConnectionString(connId));

        FullOrdersModel? order = null;
        List<FullOrdersModel>? finalOrders = null;
        IEnumerable<FullOrderItemsModel>? items = null;

        using (var lists = await conn.QueryMultipleAsync(sql, param))
        {
            while (!lists.IsConsumed)
            {
                order = lists.Read<FullOrdersModel>().Single();
                items = lists.Read<FullOrderItemsModel, ArticlesModel, FullOrderItemsModel>(
                    (orderI, article) =>
                    {
                        orderI.Article = article;
                        return orderI;
                    }, splitOn: split);
                order.OrderItems = items;
                (finalOrders ??= new()).Add(order);
            }
        }

        return finalOrders;
    }
}
