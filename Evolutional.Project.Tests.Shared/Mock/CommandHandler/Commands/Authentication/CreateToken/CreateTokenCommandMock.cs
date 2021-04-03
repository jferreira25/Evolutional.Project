using Evolutional.Project.Domain.Commands.Authentication.CreateToken;

namespace Evolutional.Project.Tests.Shared.Mock.CommandHandler.Commands.Authentication.CreateToken
{
    public static class CreateTokenCommandMock
    {
        public static CreateTokenCommand GetDefaultInstance() =>
            new CreateTokenCommand
            {
                Login = "candidato-evolucional",
                Password = "toDentro"
            };
    }
}
