using MediatR;

namespace Evolutional.Project.Domain.Commands.Users.Create
{
    public class DeleteUsersCommand : IRequest
    {
        public DeleteUsersCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
