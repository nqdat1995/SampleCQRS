using AutoMapper;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Command;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Team, Team>();
            CreateMap<TeamRequest, Team>()
                .ForMember(desc => desc.Name, act => act.MapFrom(src => src.Name))
                .ForMember(desc => desc.LogoUrl, act => act.MapFrom(src => src.LogoUrl))
                .ForMember(desc => desc.IsActive, act => act.MapFrom(src => src.IsActive));
            CreateMap<Bet, Bet>();
            CreateMap<Match, Match>();
            CreateMap<Round, Round>();
            CreateMap<Season, Season>();
            CreateMap<Tournament, Tournament>();
            CreateMap<User, User>();
            CreateMap<RegisterUserRequest, User>()
                .ForMember(desc => desc.Username, act => act.MapFrom(src => src.Email.Trim().Replace("@sacombank.com", string.Empty)))
                .ForMember(desc => desc.Password, act => act.MapFrom(src => src.Password))
                .ForMember(desc => desc.Email, act => act.MapFrom(src => src.Email));
            CreateMap<RegisterUserRequest, AddSendMailCommand>();
        }
    }
}
