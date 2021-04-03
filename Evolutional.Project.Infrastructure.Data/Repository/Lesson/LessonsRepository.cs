using Dapper;
using Evolutional.Project.CrossCutting.Configuration;
using Evolutional.Project.Domain.Interfaces;
using Evolutional.Project.Domain.Queries.Lessons.GetFilterAllLessons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Evolutional.Project.Infrastructure.Data.Repository.Lesson
{
    public class LessonsRepository : ILessonsRepository
    {
        protected IDbConnection Connection => new SqlConnection(AppSettings.Settings.Sqlconnections.ConnectionString);

        public async Task<Domain.Entities.Lesson> GetByNameAsync(string name)
        {
            var stringSql = "SELECT id,name FROM lessons WHERE name=@name";

            return await Connection.QueryFirstOrDefaultAsync<Domain.Entities.Lesson>(stringSql, new { name });
        }

        public async Task<Domain.Entities.Lesson> GetByIdAsync(long id)
        {
            var stringSql = "SELECT id,name FROM lessons WHERE id=@id";

            return await Connection.QueryFirstOrDefaultAsync<Domain.Entities.Lesson>(stringSql, new { id });
        }

        public async Task<IEnumerable<Domain.Entities.Lesson>> GetAllAsync(GetFilterAllLessonsQuery query)
        {
            var stringSql = @"SELECT id,name FROM lessons l
                                     WHERE 1 = 1
                                AND (
                                    @name IS NULL OR l.Name LIKE @name
                                )
                                ORDER BY l.Id ASC
                                OFFSET @Offset ROWS 
                                FETCH NEXT @PageLength ROWS ONLY";

            return await Connection.QueryAsync<Domain.Entities.Lesson>(stringSql, new
            {
                Name = $"%{query.Name}%",
                query.Offset,
                query.PageLength
            });
        }
        public async Task<IEnumerable<Domain.Entities.Lesson>> GetAllAsync()
        {
            var stringSql = "SELECT id,name FROM lessons ORDER BY 1 DESC";

            return await Connection.QueryAsync<Domain.Entities.Lesson>(stringSql);
        }

        public async Task<long> AddAsync(Domain.Entities.Lesson lesson)
        {
            var stringSql = new StringBuilder()
                            .Append(@"INSERT INTO lessons (name)
                                          VALUES (@Name);
                                         SELECT SCOPE_IDENTITY();");

            var id = Convert.ToInt64(await Connection.ExecuteScalarAsync(stringSql.ToString(),
                new
                {
                    lesson.Name
                }));

            return (long)id;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var stringSql = "DELETE FROM lessons WHERE ID = @id";

            await Connection.ExecuteAsync(stringSql.ToString(), new { id });

            return true;
        }

        public async Task<bool> UpdateAsync(Domain.Entities.Lesson lesson)
        {
            var stringSql = new StringBuilder()
                            .Append(@"UPDATE lessons SET
                                             name = @name
                                      WHERE ID = @Id");

            await Connection.ExecuteAsync(stringSql.ToString(), new
            {
                lesson.Name,
                lesson.Id
            });

            return true;
        }

    }
}
