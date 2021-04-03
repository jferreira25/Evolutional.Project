namespace Evolutional.Project.Tests.Shared.Mock.Domain.Entities.Users
{
    public static class UsersMock
    {
        public static Project.Domain.Entities.Users GetDefaultInstance() => new Project.Domain.Entities.Users
        {
            Id = 0,
            Name = "candidato-evolucional",
            Password = "1234"
           
        };
    }
}
