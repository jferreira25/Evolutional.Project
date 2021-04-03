using Dapper;
using Evolutional.Project.CrossCutting.Configuration;
using Evolutional.Project.Domain.Interfaces;
using Evolutional.Project.Domain.Queries.Users.GetAllUsers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Evolutional.Project.Infrastructure.Data.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        protected IDbConnection Connection => new SqlConnection(AppSettings.Settings.Sqlconnections.ConnectionString);

        public async Task<Domain.Entities.Users> GetByUserAsync(string name,string password)
        {
            var stringSql = "SELECT id,name FROM users WHERE name=@name and password=@password";

            return await Connection.QueryFirstOrDefaultAsync<Domain.Entities.Users>(stringSql, new { name, password });
        }

        public async Task<IEnumerable<Domain.Entities.Users>> GetAllAsync(GetAllUsersQuery query)
        {
            var stringSql = @"SELECT id,name FROM users  u
                                WHERE 1 = 1
                                AND (
                                    @name IS NULL OR u.Name LIKE @name
                                )
                                ORDER BY u.Id ASC
                                OFFSET @Offset ROWS 
                                FETCH NEXT @PageLength ROWS ONLY";

            return await Connection.QueryAsync<Domain.Entities.Users>(stringSql, new
            {
                Name = $"%{query.Name}%",
                query.Offset,
                query.PageLength
            });
        }
    

        public async Task<Domain.Entities.Users> GetByIdAsync(long id)
        {
            var stringSql = "SELECT id,name FROM Users WHERE id=@id";

            return await Connection.QueryFirstOrDefaultAsync<Domain.Entities.Users>(stringSql, new { id });
        }
     
        public async Task<long> AddAsync(Domain.Entities.Users users)
        {
            var stringSql = new StringBuilder()
                            .Append(@"INSERT INTO Users (name,password)
                                          VALUES (@Name,@password);
                                          SELECT SCOPE_IDENTITY();");

            var id = Convert.ToInt64(await Connection.ExecuteScalarAsync(stringSql.ToString(),
                new
                {
                    users.Name,
                    users.Password
                }));

            return (long)id;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var stringSql = "DELETE FROM Users WHERE ID = @id";

            await Connection.ExecuteAsync(stringSql.ToString(), new { id });

            return true;
        }

        public async Task<bool> UpdateAsync(Domain.Entities.Users Users)
        {
            var stringSql = new StringBuilder()
                            .Append(@"UPDATE Users SET
                                             name = @name
                                      WHERE ID = @Id");

            await Connection.ExecuteAsync(stringSql.ToString(), new
            {
                Users.Name,
                Users.Id
            });

            return true;
        }

    }
}
