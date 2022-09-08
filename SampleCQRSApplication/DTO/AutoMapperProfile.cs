using AutoMapper;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication.DTO
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
        }
    }
}
