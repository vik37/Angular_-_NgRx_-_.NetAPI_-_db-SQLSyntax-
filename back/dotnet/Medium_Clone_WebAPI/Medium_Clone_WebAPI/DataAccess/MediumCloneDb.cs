using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medium_Clone_WebAPI.DataAccess
{
    public class MediumCloneDb
    {
        private readonly IConfiguration _config;
        public MediumCloneDb(IConfiguration config)
        {
            this._config = config;
        }
    }
}
