using AutoMapper;
using Evolutional.Project.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Queries.Lessons.GetLessonsById
{
    public class GetLessonsByIdQueryHandler : IRequestHandler<GetLessonsByIdQuery, GetLessonsByIdQueryResponse>
    {
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;
        public GetLessonsByIdQueryHandler(
            ILessonsRepository lessonsRepository,
            IMapper mapper
            )
        {
            _lessonsRepository = lessonsRepository;
            _mapper = mapper;
        }


        public async Task<GetLessonsByIdQueryResponse> Handle(GetLessonsByIdQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonsRepository.GetByIdAsync(request.Id);
            return _mapper.Map<GetLessonsByIdQueryResponse>(lesson);
        }
    }
}
