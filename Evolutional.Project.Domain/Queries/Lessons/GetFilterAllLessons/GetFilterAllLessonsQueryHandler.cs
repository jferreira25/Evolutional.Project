using AutoMapper;
using Evolutional.Project.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Queries.Lessons.GetFilterAllLessons
{
    public class GetFilterAllLessonsQueryHandler : IRequestHandler<GetFilterAllLessonsQuery, GetFilterAllLessonsQueryResponse>
    {
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;
        public GetFilterAllLessonsQueryHandler(
            ILessonsRepository lessonsRepository,
            IMapper mapper
            )
        {
            _lessonsRepository = lessonsRepository;
            _mapper = mapper;
        }


        public async Task<GetFilterAllLessonsQueryResponse> Handle(GetFilterAllLessonsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Entities.Lesson> lessons;

            if(request.Offset == 0 || request == null)
               lessons=  await _lessonsRepository.GetAllAsync();
            else
            lessons = await _lessonsRepository.GetAllAsync(request);

            return _mapper.Map<GetFilterAllLessonsQueryResponse>(lessons);
        }
    }
}
