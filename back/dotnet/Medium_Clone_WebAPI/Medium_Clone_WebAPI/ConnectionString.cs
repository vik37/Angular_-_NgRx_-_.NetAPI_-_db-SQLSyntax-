using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medium_Clone_WebAPI
{
    public static class ConnectionString
    {
        public static string MCDbConnectionString { get; set; }

        //private static readonly ConnectionString _connectionString = new ConnectionString(_config);
        //private readonly IConfiguration _config;
        //private ConnectionString(IConfiguration config) 
        //{
        //    this._config = config;
        //}
        //public static ConnectionString GetInstance()
        //{   

        //    return _connectionString;
        //}
        //public string GetConnectionStringFromMediumCloneDb()
        //{

        //    return _config.GetConnectionString("MCConnectionString");
        //}

    }
}
