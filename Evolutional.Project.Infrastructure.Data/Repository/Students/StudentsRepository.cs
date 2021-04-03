using Dapper;
using Evolutional.Project.CrossCutting.Configuration;
using Evolutional.Project.Domain.Interfaces;
using Evolutional.Project.Domain.Queries.Students.GetAllStudents;
using Evolutional.Project.Domain.Queries.Users.GetAllUsers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Evolutional.Project.Infrastructure.Data.Repository.Students
{
    public class StudentsRepository : IStudentsRepository
    {
        protected IDbConnection Connection => new SqlConnection(AppSettings.Settings.Sqlconnections.ConnectionString);

        public async Task<Domain.Entities.Students> GetByNameAsync(string name)
        {
            var stringSql = "SELECT id,name FROM students WHERE name=@name";

            return await Connection.QueryFirstOrDefaultAsync<Domain.Entities.Students>(stringSql, new { name });
        }

        public async Task<IEnumerable<Domain.Entities.Students>> GetAllAsync(string name, int offSet, int pageLength)
        {
            var stringSql = @"SELECT S.ID,S.[NAME] AS 'NAME',SL.[SchoolGrades] AS 'SchoolGrades',l.name AS 'lessonName' FROM [dbo].[students] S
                                INNER JOIN [dbo].[students_lessons] SL ON SL.STUDENTSID = S.ID 
                                INNER JOIN [dbo].[lessons] L ON L.ID = SL.LESSONSID 
                              WHERE 1 = 1
                                AND (
                                    @name IS NULL OR S.Name LIKE @name
                                )
                                ORDER BY S.name ASC
                                OFFSET @Offset ROWS 
                                FETCH NEXT @PageLength ROWS ONLY";

            return await Connection.QueryAsync<Domain.Entities.Students>(stringSql, new
            {
                Name = $"%{name}%",
                offSet,
                pageLength
            });
        }

        public async Task<IEnumerable<Domain.Entities.Students>> GetAllAsync()
        {
            var stringSql = @"SELECT
                                    S.ID,S.[NAME] AS 'NAME',SL.[SchoolGrades] AS 'SchoolGrades',L.name AS 'LessonName' FROM [dbo].[students] S
                              INNER JOIN [dbo].[students_lessons] SL ON SL.STUDENTSID = S.ID 
                              INNER JOIN [dbo].[lessons] L ON L.ID = SL.LessonsId"; 

            return await Connection.QueryAsync<Domain.Entities.Students>(stringSql);
        }


        public async Task<Domain.Entities.Students> GetByIdAsync(long id)
        {
            var stringSql = @"SELECT S.ID,S.[NAME] AS 'NAME',SL.[SchoolGrades] AS 'SchoolGrades' 
                                FROM [dbo].[students] S
                                INNER JOIN[dbo].[students_lessons] SL ON SL.STUDENTSID = S.ID
                              WHERE S.ID =@id";

            return await Connection.QueryFirstOrDefaultAsync<Domain.Entities.Students>(stringSql, new { id });
        }

        public async Task<long> AddAsync(Domain.Entities.Students students)
        {
            var stringSql = new StringBuilder()
                            .Append(@"INSERT INTO students (name)
                                          VALUES (@Name);
                                          SELECT SCOPE_IDENTITY();");

            var id = Convert.ToInt64(await Connection.ExecuteScalarAsync(stringSql.ToString(),
                new
                {
                    students.Name
                   
                }));

            return (long)id;
        }


        public async Task<bool> DeleteAsync(long id)
        {
            var stringSql = "DELETE FROM students WHERE ID = @id";

            await Connection.ExecuteAsync(stringSql.ToString(), new { id });

            return true;
        }

        public async Task<bool> UpdateAsync(Domain.Entities.Students students)
        {
            var stringSql = new StringBuilder()
                            .Append(@"UPDATE students SET
                                             name = @name
                                      WHERE ID = @Id");

            await Connection.ExecuteAsync(stringSql.ToString(), new
            {
                students.Name,
                students.Id
            });

            return true;
        }

    }
}
