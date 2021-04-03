
using AutoMapper;
using Evolutional.Project.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Commands.Users.Create
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand, CreateUsersCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CreateUsersCommandHandler(
            IUserRepository userRepository,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<CreateUsersCommandResponse> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Entities.Users>(request);

            var id = await _userRepository.AddAsync(user);

            return new CreateUsersCommandResponse(id);
        }
    }
}
