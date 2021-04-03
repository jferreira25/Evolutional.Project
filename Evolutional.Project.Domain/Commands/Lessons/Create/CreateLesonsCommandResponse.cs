namespace Evolutional.Project.Domain.Commands.Lessons.Create
{
    public class CreateLesonsCommandResponse
    {
        public CreateLesonsCommandResponse(long id)
        {
            this.id = id;
        }

        public long id { get; set; }
    }
}
