using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Dapper;

public class DapperContext
{
    private readonly string _ConnectionString;

    public DapperContext(string connectionString)
    {
        _ConnectionString = connectionString;
    }

    public IDbConnection CreateConnection()
       => new SqlConnection(_ConnectionString);

    public string Inventories => "[Seller].Inventories";
}
