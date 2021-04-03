using AutoMapper;
using Evolutional.Project.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Commands.Lessons.Update
{
    public class UpdateLessonsCommandHandler : IRequestHandler<UpdateLessonsCommand, Unit>
    {
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;
        public UpdateLessonsCommandHandler(
            ILessonsRepository lessonsRepository,
            IMapper mapper
            )
        {
            _lessonsRepository = lessonsRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLessonsCommand request, CancellationToken cancellationToken)
        {
            var lesson = _mapper.Map<Entities.Lesson>(request);
            await _lessonsRepository.UpdateAsync(lesson);

            return Unit.Value;
        }
    }
}
