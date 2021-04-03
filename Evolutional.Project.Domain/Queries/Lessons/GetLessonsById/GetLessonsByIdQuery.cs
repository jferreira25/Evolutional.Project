using MediatR;

namespace Evolutional.Project.Domain.Queries.Lessons.GetLessonsById
{
    public class GetLessonsByIdQuery : IRequest<GetLessonsByIdQueryResponse>
    {
        public long Id { get; set; }

        public GetLessonsByIdQuery(long id)
        {
            Id = id;
        }
    }
}
