using AutoMapper;
using Evolutional.Project.Domain.Commands.Users.Create;
using Evolutional.Project.Domain.Entities;
using Evolutional.Project.Domain.Queries.Users.GetAllUsers;
using Evolutional.Project.Domain.Queries.Users.GetUsersById;
using System.Collections.Generic;

namespace Evolutional.Project.Domain.MappersProfile
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<CreateUsersCommand, Users>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateUsersCommand, Users>();
            CreateMap<Users, GetUsersByIdQueryResponse>();

            CreateMap<Users, GetUsersQueryResponse>();
            CreateMap<Users, GetAllUsersQueryResponse>();

            CreateMap<List<Users>, GetAllUsersQueryResponse> ()
             .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src))
               .ForMember(dest => dest.TotalRows, opt => opt.MapFrom(src => src.Count));
        }
    }
}
