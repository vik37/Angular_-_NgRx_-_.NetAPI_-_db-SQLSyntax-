using Entities;
using Medium_Clone_WebAPI.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper.Contrib.Extensions;

namespace Medium_Clone_WebAPI.DataAccess.Repositories
{
    public class CurrentUserRepo : IRepository<CurentUser>
    {
        public IEnumerable<CurentUser> GetAll()
        {
            var users = new List<CurentUser>();
            using (var connection = new SqlConnection(ConnectionString.MCDbConnectionString))
            {
                connection.Open();
                using (var multi = connection.QueryMultiple("SelectAllCurentUsers", commandType: CommandType.StoredProcedure))
                {
                    users = multi.Read<CurentUser>().ToList();
                    
                }
                connection.Close();
            }
            return users;
        }

        public CurentUser GetById(int id)
        {
            var user = new CurentUser();
            using (var connection = new SqlConnection(ConnectionString.MCDbConnectionString))
            {
                connection.Open();
                user = connection.QuerySingleOrDefault<CurentUser>("SelectCurentUserById", new { Id = id }, commandType: CommandType.StoredProcedure);
                connection.Close();
            }
            return user;
        }
        public void Add(CurentUser user)
        {
            using (var connection = new SqlConnection(ConnectionString.MCDbConnectionString))
            {
                connection.Open();
                var parameter = new
                {
                    username = user.Username,
                    email = user.Email,
                    password = user.Password,
                    createdAt = user.CreatedAt,
                    updateAt = user.UpdateAt,
                    bio = user.Bio,
                    image = user.Image

                };
                connection.ExecuteScalar<CurentUser>("AddCurentUser", parameter, commandType: CommandType.StoredProcedure);
                connection.Close();
            }
        }
    }
}
