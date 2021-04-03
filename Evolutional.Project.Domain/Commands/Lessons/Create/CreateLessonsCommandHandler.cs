
using AutoMapper;
using Evolutional.Project.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Commands.Lessons.Create
{
    public class CreateLessonsCommandHandler : IRequestHandler<CreateLessonsCommand, CreateLesonsCommandResponse>
    {
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;
        public CreateLessonsCommandHandler(
            ILessonsRepository lessonsRepository,
        IMapper mapper
            )
        {
            _lessonsRepository = lessonsRepository;
            _mapper = mapper;
        }
        public async Task<CreateLesonsCommandResponse> Handle(CreateLessonsCommand request, CancellationToken cancellationToken)
        {
            var lesson = _mapper.Map<Entities.Lesson>(request);

            var id = await _lessonsRepository.AddAsync(lesson);

            return new CreateLesonsCommandResponse(id);
        }
    }
}
