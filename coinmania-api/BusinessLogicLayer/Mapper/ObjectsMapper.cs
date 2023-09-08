using AutoMapper;
using BusinessLogicLayer.Commands;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Mapper;

public class ObjectsMapper : Profile
{
    public ObjectsMapper()
    {
        CreateMap<LogInCommand, LogInUser>().ReverseMap();
        CreateMap<RegisterUserCommand, User>()
            .ForMember(d => d.UserName, m => m.MapFrom(o => o.UserName))
            .ForMember(d => d.PhoneNumber, m => m.MapFrom(o => o.UserName))
            .ForMember(d => d.GeneralInformation, m => m.MapFrom(o => new UserInformation()
            {
                FirstName = o.FirstName,
                LastName = o.LastName,
                Phone = o.UserName
            })).ReverseMap();
        CreateMap<GetUsersCommand, IList<User>>().ReverseMap();
        CreateMap<FindUserCommand, bool>().ReverseMap();
    }
}