using AutoMapper;
using Evolutional.Project.Domain.Commands.Lessons.Create;
using Evolutional.Project.Domain.Commands.Lessons.Update;
using Evolutional.Project.Domain.Entities;
using Evolutional.Project.Domain.Queries.Lessons.GetFilterAllLessons;
using Evolutional.Project.Domain.Queries.Lessons.GetLessonsById;
using System.Collections.Generic;

namespace Evolutional.Project.Domain.MappersProfile
{
    public class lessonsProfile : Profile
    {
        public lessonsProfile()
        {

            CreateMap<Lesson, GetFilterAllLessonsQueryResponse>();

            CreateMap<Lesson, GetLessonsByIdQueryResponse>();
            CreateMap<List<Lesson>, GetFilterAllLessonsQueryResponse>()
             .ForMember(dest => dest.Lessons, opt => opt.MapFrom(src => src))
             .ForMember(dest => dest.TotalRows, opt => opt.MapFrom(src => src.Count));

            CreateMap<CreateLessonsCommand, Lesson>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateLessonsCommand, Lesson>();
        }
    }
}
