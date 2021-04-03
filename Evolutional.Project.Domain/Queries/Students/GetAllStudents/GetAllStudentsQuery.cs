using MediatR;

namespace Evolutional.Project.Domain.Queries.Students.GetAllStudents
{
    public class GetAllStudentsQuery : BaseQuery, IRequest<GetAllStudentsQueryResponse>
    {
        public string Name { get; set; }
    }
}
