namespace Evolutional.Project.Domain.Commands.Users.Create
{
    public class CreateUsersCommandResponse
    {
        public CreateUsersCommandResponse(long id)
        {
            this.id = id;
        }

        public long id { get; set; }
    }
}
