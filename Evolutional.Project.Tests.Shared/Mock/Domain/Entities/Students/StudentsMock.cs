namespace Evolutional.Project.Tests.Shared.Mock.Domain.Entities.Students
{
    public static class StudentsMock
    {
        public static Evolutional.Project.Domain.Entities.Students GetDefaultInstance() => new Evolutional.Project.Domain.Entities.Students
        {
            Id = 0,
            Name = "Mock"
        };
    }
}
