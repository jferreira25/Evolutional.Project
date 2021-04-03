using Dapper;
using Evolutional.Project.CrossCutting.Configuration;
using Evolutional.Project.Domain.Dto;
using Evolutional.Project.Domain.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Evolutional.Project.Infrastructure.Data.Repository.StudentsLessons
{
    public class StudentsLessonsProcedure : IStudentsLessonsProcedure
    {
        protected IDbConnection Connection => new SqlConnection(AppSettings.Settings.Sqlconnections.ConnectionString);
        public async Task<long> AddAsync(StudentsLessonsDto studentsLessons)
        {
            var values = new { StudentName = studentsLessons.StudentName, LessonId = studentsLessons.LessonId, SchoolGrades = studentsLessons.SchoolGrades };

            var id = Convert.ToInt64(await Connection.ExecuteScalarAsync("Insert_Students_And_Lessons", values, commandType: CommandType.StoredProcedure));

            return (long)id;
        }
    }
}
