using Evolutional.Project.Domain.Interfaces;
using Evolutional.Project.Domain.Interfaces.Tools;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Commands.Authentication.CreateToken
{
    public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, CreateTokenCommandResponse>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public CreateTokenCommandHandler(
           IJwtTokenGenerator jwtTokenGenerator,
           IUserRepository userRepository
            )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<CreateTokenCommandResponse> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserAsync(request.Login, request.Password);

            if (user == null)
                throw new System.Exception("401");

            var token = _jwtTokenGenerator.GenerateToken(request.Login);
            return new CreateTokenCommandResponse(token);
        }
    }
}
