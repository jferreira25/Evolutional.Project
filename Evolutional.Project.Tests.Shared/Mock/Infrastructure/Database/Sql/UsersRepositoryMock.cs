using Evolutional.Project.Domain.Entities;
using Evolutional.Project.Domain.Interfaces;
using Evolutional.Project.Domain.Queries.Users.GetAllUsers;
using Evolutional.Project.Tests.Shared.Core;
using Evolutional.Project.Tests.Shared.Mock.Domain.Entities.Users;
using Moq;
using System.Collections.Generic;

namespace Evolutional.Project.Tests.Shared.Mock.Infrastructure.Database.Sql
{
    public class UsersRepositoryMock : BaseMock<IUserRepository>
    {
        public override Mock<IUserRepository> GetDefaultInstance()
        {
            UserRepository();
            return Mock;
        }

        private void UserRepository()
        {
            Setup(r => r.AddAsync(It.IsAny<Users>()), 1);
            Setup(r => r.UpdateAsync(It.IsAny<Users>()), true);
            Setup(r => r.DeleteAsync(It.IsAny<long>()), true);
            Setup(r => r.GetByIdAsync(It.IsAny<long>()), UsersMock.GetDefaultInstance());
            Setup(r => r.GetByUserAsync(It.IsAny<string>(), It.IsAny<string>()), UsersMock.GetDefaultInstance());
            Setup(r => r.GetAllAsync(It.IsAny<GetAllUsersQuery>()), new List<Users>());

        }
    }
}
