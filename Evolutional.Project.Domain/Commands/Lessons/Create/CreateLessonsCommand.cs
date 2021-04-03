using MediatR;

namespace Evolutional.Project.Domain.Commands.Lessons.Create
{
    public class CreateLessonsCommand : IRequest<CreateLesonsCommandResponse>
    {
        public string Name { get; set; }
     
    }
}
