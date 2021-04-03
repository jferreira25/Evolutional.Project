using Evolutional.Project.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Commands.Users.Create
{
    public class DeleteUsersCommandHandler : IRequestHandler<DeleteUsersCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUsersCommandHandler(
            IUserRepository userRepository

            )
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
