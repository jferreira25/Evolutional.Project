using Evolutional.Project.Infrastructure.Data.Repository.Users;
using Evolutional.Project.Tests.Shared.Mock.Domain.Entities.Users;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Transactions;

namespace Evolutional.Project.Tests.Integration.Infrastructure.DataBase.Sql.Repositories
{
    class UsersRepositoryTest
    {
        private TransactionScope _transactionScope;
       
        private readonly UserRepository _userRepository;

        public UsersRepositoryTest()
        {
            _userRepository = new UserRepository();
        }

        [SetUp]
        public void TransactionSetUp()
        {
            _transactionScope = CrossCutting.Configuration.
                Transaction.GetTransactionAsync(60);
        }

        [TearDown]
        public void TransactionTearDown()
        {
            _transactionScope.Dispose();
        }

        [TestCase(TestName = "Success Test Add Users")]
        public async Task Test_AddUsers()
        {
            var id = await InsertUsers();
            id.Should().BeGreaterThan(0);
        }

        [TestCase(TestName = "Success Test GetAll Users")]
        public async Task Test_GetAllUsers()
        {
            var Users = await _userRepository.GetAllAsync(new Domain.Queries.Users.GetAllUsers.GetAllUsersQuery() { PageLength = 10,CurrentPage=0});
            Users.Should().HaveCountGreaterThan(0);
        }

        [TestCase(TestName = "Sucess Test Update Users")]
        public async Task Test_UpdateUsersAsync()
        {
            var Users = UsersMock.GetDefaultInstance();
            Users.Id = await _userRepository.AddAsync(Users);

            Users.Name = "testando mock update";

            await _userRepository.UpdateAsync(Users);

            var UsersUpdated = await _userRepository.GetByIdAsync(Users.Id);

            UsersUpdated.Name.Should().Be("testando mock update");
        }

        [TestCase(TestName = "Sucess Test Delete Users")]
        public async Task Test_DeleteUsersAsync()
        {
            var Users = UsersMock.GetDefaultInstance();
            Users.Id = await _userRepository.AddAsync(Users);
           
            Users.Name = "testando mock delete";

            await _userRepository.DeleteAsync(Users.Id);

            var UsersIsDeleted = await _userRepository.DeleteAsync(Users.Id);
            UsersIsDeleted.Should().BeTrue();

        }

        private async Task<long> InsertUsers()
        {
            var Users = UsersMock.GetDefaultInstance();
            var id = await _userRepository.AddAsync(Users);
            return id;
        }
    }
}
