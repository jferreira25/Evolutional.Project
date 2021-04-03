using Evolutional.Project.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Commands.Students.Create
{
    public class DeleteStudentsCommandHandler : IRequestHandler<DeleteStudentsCommand, Unit>
    {
        private readonly IStudentsRepository _studentsRepository;

        public DeleteStudentsCommandHandler(
            IStudentsRepository studentsRepository

            )
        {
            _studentsRepository = studentsRepository;
        }

        public async Task<Unit> Handle(DeleteStudentsCommand request, CancellationToken cancellationToken)
        {
            await _studentsRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
