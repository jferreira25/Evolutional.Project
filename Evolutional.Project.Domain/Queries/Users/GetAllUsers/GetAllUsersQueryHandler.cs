using AutoMapper;
using Evolutional.Project.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Queries.Users.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersQueryResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(
            IUserRepository userRepository,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(request);
            return _mapper.Map<GetAllUsersQueryResponse>(users);
        }
    }
}
