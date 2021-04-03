using MediatR;

namespace Evolutional.Project.Domain.Commands.Lessons.Delete
{
    public class DeleteLessonsCommand : IRequest
    {
        public DeleteLessonsCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
