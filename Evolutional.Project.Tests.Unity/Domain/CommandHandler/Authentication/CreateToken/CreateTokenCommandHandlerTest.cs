using Evolutional.Project.Domain.Commands.Authentication.CreateToken;
using Evolutional.Project.Tests.Shared.Mock.CommandHandler.Commands.Authentication.CreateToken;
using Evolutional.Project.Tests.Shared.Mock.Infrastructure.Database.Sql;
using Evolutional.Project.Tests.Shared.Mock.Tools;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Tests.Unity.Domain.CommandHandler.Authentication.CreateToken
{
    public class CreateTokenCommandHandlerTest
    {
        protected CreateTokenCommandHandler EstablishContext() => new CreateTokenCommandHandler(
               new JwtTokenGeneratorMock().GetDefaultInstance().Object,
               new UsersRepositoryMock().GetDefaultInstance().Object
        );

        [SetUp]
        public void Setup()
        {
        }

        [TestCase(Category = "Unity", TestName = "Should return the token successfully")]
        public async Task Should_Return_Token_Successfully()
        {
            var request = CreateTokenCommandMock.GetDefaultInstance();

            var response = await EstablishContext().Handle(request, CancellationToken.None);

            Assert.IsNotNull(response);
        }
    }
}
