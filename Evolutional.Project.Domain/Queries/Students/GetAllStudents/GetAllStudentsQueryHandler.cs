using AutoMapper;
using Evolutional.Project.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Queries.Students.GetAllStudents
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, GetAllStudentsQueryResponse>
    {
        private readonly IStudentsRepository  _studentsRepository;
        private readonly IMapper _mapper;
        public GetAllStudentsQueryHandler(
            IStudentsRepository lessonsRepository,
            IMapper mapper
            )
        {
            _studentsRepository = lessonsRepository;
            _mapper = mapper;
        }


        public async Task<GetAllStudentsQueryResponse> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentsRepository.GetAllAsync(request.Name, request.Offset, request.PageLength);
            return _mapper.Map<GetAllStudentsQueryResponse>(students);
        }
    }
}
