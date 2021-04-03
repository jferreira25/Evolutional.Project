using Evolutional.Project.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Commands.Lessons.Delete
{
    public class DeleteLessonsCommandHandler : IRequestHandler<DeleteLessonsCommand, Unit>
    {
        private readonly ILessonsRepository _lessonsRepository;

        public DeleteLessonsCommandHandler(
            ILessonsRepository lessonsRepository

            )
        {
            _lessonsRepository = lessonsRepository;
        }

        public async Task<Unit> Handle(DeleteLessonsCommand request, CancellationToken cancellationToken)
        {
            await _lessonsRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
