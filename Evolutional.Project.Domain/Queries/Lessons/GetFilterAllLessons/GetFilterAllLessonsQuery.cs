using MediatR;

namespace Evolutional.Project.Domain.Queries.Lessons.GetFilterAllLessons
{
    public class GetFilterAllLessonsQuery : BaseQuery, IRequest<GetFilterAllLessonsQueryResponse>
    {
        public string Name { get; set; }
    }
}
