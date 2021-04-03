using MediatR;

namespace Evolutional.Project.Domain.Commands.Users.Create
{
    public class CreateUsersCommand: IRequest<CreateUsersCommandResponse>
    {
        public string Name { get; set; }
        public string Password { get; set; }
      
    }
}
