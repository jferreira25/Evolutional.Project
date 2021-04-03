using MediatR;

namespace Evolutional.Project.Domain.Queries.Users.GetUsersById
{
    public class GetUsersByIdQuery : IRequest<GetUsersByIdQueryResponse>
    {
        public long Id { get; set; }

        public GetUsersByIdQuery(long id)
        {
            Id = id;
        }
    }
}
