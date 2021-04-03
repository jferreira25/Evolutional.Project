using System.Collections.Generic;

namespace Evolutional.Project.Domain.Queries.Students.GetAllStudents
{
    public class GetAllStudentsQueryResponse
    {
        public List<GetStudentsResponseData> Students { get; set; }
        public long TotalRows { get; set; }
    }
    public class GetStudentsResponseData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal SchoolGrades { get; set; }
        public string LessonName { get; set; }

    }
}
