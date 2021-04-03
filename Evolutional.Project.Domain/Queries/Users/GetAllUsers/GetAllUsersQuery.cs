using MediatR;

namespace Evolutional.Project.Domain.Queries.Users.GetAllUsers
{
    public class GetAllUsersQuery : BaseQuery, IRequest<GetAllUsersQueryResponse>
    {
        public string Name { get; set; }
    }
}
