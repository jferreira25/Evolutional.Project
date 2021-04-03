using MediatR;

namespace Evolutional.Project.Domain.Commands.Students.Create
{
    public class UpdateStudentsCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SchoolGrades { get; set; }
    }
}
