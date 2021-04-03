using System.Collections.Generic;

namespace Evolutional.Project.Domain.Queries.Users.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public List<GetUsersQueryResponse> Users { get; set; }
        public long TotalRows { get; set; }
    }
    public class GetUsersQueryResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
